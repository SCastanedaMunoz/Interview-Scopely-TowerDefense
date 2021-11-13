using System.Collections;
using TowerDefense.Creeps;
using TowerDefense.Projectiles;
using UnityEngine;

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

        private void Awake()
        {
            _turretTransform = transform;
        }

        private void Start()
        {
            // set an initial aim point
            if (!_aiming)
                _aimPoint = _turretTransform.TransformPoint(Vector3.forward * 100f);
            StartCoroutine(ScanForEnemies());
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
                data.rotationRate * Time.deltaTime);

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
                var closestCreep = Creep.GetClosestCreep(_turretTransform.position, data.range);

                if (closestCreep != null)
                {
                    _aiming = true;
                    var position = closestCreep.CreepTransform.position;
                    _aimPoint = position;

                    var forward = turretBody.TransformDirection(Vector3.forward);
                    var toOther = (position - turretBody.position).normalized;

                    // assume we are close enough to shoot 
                    if (!(Vector3.Dot(forward, toOther) >= AimThreshold))
                        continue;

                    OnCreepInSight(closestCreep);
                    yield return new WaitForSeconds(data.shootRate);
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
            Gizmos.DrawWireSphere(transform.position, data.range);
        }

        public static void Create(Turret turret, Vector3 spawnPosition)
        {
            Instantiate(turret, spawnPosition, Quaternion.identity);
        }
    }
}