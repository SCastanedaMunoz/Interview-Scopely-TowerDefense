using System.Collections;
using TowerDefense.Creeps;
using TowerDefense.Projectiles;
using TowerDefense.Upgrades;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Turrets
{
    /// <summary>
    /// simple turret
    /// </summary>
    public class Turret : MonoBehaviour
    {
        /// <summary>
        /// turret data
        /// </summary>
        public TurretData data;

        /// <summary>
        /// where projectiles come from
        /// </summary>
        public Transform shootPosition;

        /// <summary>
        /// turret rotational body
        /// </summary>
        public Transform turretBody;

        /// <summary>
        /// cached turret transform
        /// </summary>
        private Transform _turretTransform;

        /// <summary>
        /// creep scan threshold :)
        /// </summary>
        private const float CreepScanThreshold = 0.1f;

        /// <summary>
        /// creep aim threshold. (How accurate we need to be aiming in order to shoot :D)
        /// </summary>
        private const float AimThreshold = 0.9f;

        /// <summary>
        /// current aimPoint
        /// </summary>
        private Vector3 _aimPoint;

        /// <summary>
        /// turret aim status
        /// </summary>
        private bool _aiming = false;
        
        /// <summary>
        /// let's make creep's avoid turrets
        /// </summary>
        private NavMeshObstacle _turretObstacle;

        /// <summary>
        /// used to detect turret placemnt
        /// </summary>
        private Collider _turretCollider;

        /// <summary>
        /// how often the turret shoots
        /// </summary>
        public float ShootRate { get; private set; }
        
        /// <summary>
        /// how fast the turret turns
        /// </summary>
        public float TurnSpeed { get; private set; }
        
        /// <summary>
        /// turret range
        /// </summary>
        public float Range { get; private set; }

        private Coroutine _scanForEnemiesCoroutine;

        private void Awake()
        {
            _turretTransform = transform;
            _turretObstacle = GetComponent<NavMeshObstacle>();
            _turretCollider = GetComponent<Collider>();

            // disable collides on startup, for preview purposes
            _turretObstacle.enabled = false;
            _turretCollider.enabled = false;

            // get upgrades
            var shootRateUpgrade = UpgradeHandler.Instance.shootRate;
            var turnRateUpgrade = UpgradeHandler.Instance.turnSpeed;
            var rangeUpgrade = UpgradeHandler.Instance.range;

            // need to account for the fact that turrets can be added after upgrades
            // do not want to touch scriptable objects, they should be mere initial data sets.
            ShootRate = data.shootRate - data.shootRateUpgradeRate * shootRateUpgrade.TimesUpgraded;
            TurnSpeed = data.rotationRate + data.rotationRateUpgradeRate * turnRateUpgrade.TimesUpgraded;
            Range = data.range + data.rangeUpgradeRate * rangeUpgrade.TimesUpgraded;

            // listen for new upgrades
            shootRateUpgrade.onUpgrade.AddListener(OnShootRateUpgraded);
            turnRateUpgrade.onUpgrade.AddListener(OnTurnSpeedUpgraded);
            rangeUpgrade.onUpgrade.AddListener(OnRangeUpgraded);
        }

        private void Start()
        {
            // set an initial aim point
            if (!_aiming)
                _aimPoint = _turretTransform.TransformPoint(Vector3.forward * 100f);
            GameManager.Instance.onGamePause.AddListener(OnGamePause);
            GameManager.Instance.onGameOver.AddListener(OnGameOver);
        }
        
        private void Update()
        {
            if (_aiming)
                RotateTurret();
        }

        #region TURRET LOGIC
        /// <summary>
        /// activate's turret >:D
        /// </summary>
        public void Activate()
        {
            _turretObstacle.enabled = true;
            _turretCollider.enabled = true;
            _scanForEnemiesCoroutine = StartCoroutine(ScanForEnemies());
        }

        /// <summary>
        /// handles turret rotation
        /// </summary>        
        private void RotateTurret()
        {
            if (turretBody == null)
            {
                Debug.LogError("cannot aim, turret body hasn't been set");
                return;
            }

            // get local position
            var localTargetPos = _turretTransform.InverseTransformPoint(_aimPoint);
            localTargetPos.y = 0.0f;

            // where we want to be aiming at
            var rotationGoal = Quaternion.LookRotation(localTargetPos);
            
            // actual rotation logic
            var newRotation = Quaternion.RotateTowards(turretBody.localRotation, rotationGoal,
                TurnSpeed * Time.deltaTime);

            turretBody.localRotation = newRotation;
        }

        private IEnumerator ScanForEnemies()
        {
            // this means we will be executing enemy scanning infinitely.
            while (true)
            {
                // there is an issue here that basically we are delaying our turret shot by the scan threshold,
                // if this threshold is increased a lot, it will delay shoot time significantly
                // how often should we wait to scan for a new closer enemy
                yield return new WaitForSeconds(CreepScanThreshold);
                yield return new WaitUntil(() => !GameManager.Instance.IsGamePaused);
                // detect closet creep
                var closestCreep = Creep.GetClosestCreep(_turretTransform.position, Range);
                if (closestCreep != null) {
                    _aiming = true;
                    
                    // get creep's current postion
                    var position = closestCreep.CreepTransform.position;
                    _aimPoint = position;

                    var forward = turretBody.TransformDirection(Vector3.forward);
                    var toOther = (position - turretBody.position).normalized;

                    // detect how close we are to shoot
                    if (!(Vector3.Dot(forward, toOther) >= AimThreshold))
                        continue;

                    OnCreepInSight(closestCreep);
                    yield return new WaitForSeconds(ShootRate);
                }
                else
                    _aiming = false;
            }
        }

        // todo - account for turrets to be able to shoot more than just projectiles maybe lasers
        private void OnCreepInSight(Creep creep)
        {
            // create projectile
            Projectile.Create(data.projectile, shootPosition.position, creep);
        }
        #endregion

        #region GAME EVENTS
        private void OnGamePause(bool isPaused)
        {
            _aiming = !isPaused;
        }
        
        private void OnGameOver(bool isWIn)
        {
            _aiming = false;
            if (_scanForEnemiesCoroutine != null)
                StopCoroutine(_scanForEnemiesCoroutine);
        }
        #endregion

        #region UPGRADES
        /// <summary>
        /// called when shoot rate is upgraded
        /// </summary>
        private void OnShootRateUpgraded()
        {
            // shoot rate upgrades need to be limited
            if (ShootRate >= 0.1f)
                // an actual upgrade logic would go here ¯\_(ツ)_/¯
                ShootRate -= data.shootRateUpgradeRate;
        }

        /// <summary>
        /// called when turn rate is upgraded
        /// </summary>
        private void OnTurnSpeedUpgraded()
        {
            // an actual upgrade logic would go here ¯\_(ツ)_/¯
            TurnSpeed += data.rotationRateUpgradeRate;
        }
        
        /// <summary>
        /// called when range is upgraded
        /// </summary>
        private void OnRangeUpgraded()
        {
            // an actual upgrade logic would go here ¯\_(ツ)_/¯
            Range += data.rangeUpgradeRate;
        }
        #endregion

        /// <summary>
        /// let me see turret range
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
        }

        /// <summary>
        /// easy create
        /// </summary>
        /// <param name="turret">prefab</param>
        /// <param name="spawnPosition">spawn position</param>
        /// <returns>created turret</returns>
        public static Turret Create(Turret turret, Vector3 spawnPosition)
        {
           return Instantiate(turret, spawnPosition, Quaternion.identity);
        }
    }
}