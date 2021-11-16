using System.Collections;
using TowerDefense.Creeps;
using TowerDefense.Projectiles;
using TowerDefense.Upgrades;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense.Turrets
{
    public class Turret : MonoBehaviour
    {
        public TurretData data;

        public Transform shootPosition;

        public Transform turretBody;

        private Transform _turretTransform;

        private const float CreepScanThreshold = 0.1f;

        private const float AimThreshold = 0.9f;

        public bool Idle => !_aiming;

        private Vector3 _aimPoint;

        private bool _aiming = false;

        private NavMeshObstacle _turretObstacle;

        private Collider _turretCollider;

        public float ShootRate { get; private set; }
        public float TurnSpeed { get; private set; }
        public float Range { get; private set; }

        private void Awake()
        {
            _turretTransform = transform;
            _turretObstacle = GetComponent<NavMeshObstacle>();
            _turretCollider = GetComponent<Collider>();

            _turretObstacle.enabled = false;
            _turretCollider.enabled = false;

            var shootRateUpgrade = UpgradeHandler.Instance.shootRate;
            var turnRateUpgrade = UpgradeHandler.Instance.turnSpeed;
            var rangeUpgrade = UpgradeHandler.Instance.range;

            // need to account for the fact that turrets can be added after upgrades
            // do not want to touch scriptable objects, they should be mere initial data sets.
            ShootRate = data.shootRate - data.shootRateUpgradeRate * shootRateUpgrade.TimesUpgraded;
            TurnSpeed = data.rotationRate + data.rotationRateUpgradeRate * turnRateUpgrade.TimesUpgraded;
            Range = data.range + data.rangeUpgradeRate * rangeUpgrade.TimesUpgraded;

            shootRateUpgrade.onUpgrade.AddListener(OnShootRateUpgraded);
            turnRateUpgrade.onUpgrade.AddListener(OnTurnSpeedUpgraded);
            rangeUpgrade.onUpgrade.AddListener(OnRangeUpgraded);
        }

        private void OnShootRateUpgraded()
        {
            // shoot rate upgrades need to be limited
            if (ShootRate >= 0.1f)
                // an actual upgrade logic would go here ¯\_(ツ)_/¯
                ShootRate -= data.shootRateUpgradeRate;
        }

        private void OnTurnSpeedUpgraded()
        {
            // an actual upgrade logic would go here ¯\_(ツ)_/¯
            TurnSpeed += data.rotationRateUpgradeRate;
        }
        
        private void OnRangeUpgraded()
        {
            // an actual upgrade logic would go here ¯\_(ツ)_/¯
            Range += data.rangeUpgradeRate;
        }
        
        private void Start()
        {
            // set an initial aim point
            if (!_aiming)
                _aimPoint = _turretTransform.TransformPoint(Vector3.forward * 100f);
            GameManager.Instance.onGamePause.AddListener(OnGamePause);
        }

        public void Activate()
        {
            _turretObstacle.enabled = true;
            _turretCollider.enabled = true;
            StartCoroutine(ScanForEnemies());
        }

        private void OnGamePause(bool isPaused)
        {
            _aiming = !isPaused;
        }

        private void Update()
        {
            if (_aiming)
                RotateTurret();
        }

        private void RotateTurret()
        {
            if (turretBody == null)
            {
                Debug.LogError("cannot aim, turret body hasn't been set");
                return;
            }

            var localTargetPos = _turretTransform.InverseTransformPoint(_aimPoint);
            localTargetPos.y = 0.0f;

            var rotationGoal = Quaternion.LookRotation(localTargetPos);
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
                var closestCreep = Creep.GetClosestCreep(_turretTransform.position, Range);
                yield return new WaitUntil(() => !GameManager.Instance.IsGamePaused);
                if (closestCreep != null) {
                    _aiming = true;
                    var position = closestCreep.CreepTransform.position;
                    _aimPoint = position;

                    var forward = turretBody.TransformDirection(Vector3.forward);
                    var toOther = (position - turretBody.position).normalized;

                    // assume we are close enough to shoot 
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
        }

        public static Turret Create(Turret turret, Vector3 spawnPosition)
        {
           return Instantiate(turret, spawnPosition, Quaternion.identity);
        }
    }
}