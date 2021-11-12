using System.Collections;
using TowerDefense.Creeps;
using UnityEngine;

namespace TowerDefense.Turrets
{
    public class Turret : MonoBehaviour
    {
        public TurretData data; 
        
        public Transform _shootPosition;
        
        private Transform _turretTransform;

        private void Awake()
        {
            _turretTransform = transform;
        }

        private void Start()
        {
            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            yield return new WaitForSeconds(data.shootRate);
            
            var closestCreep = Creep.GetClosestCreep(_turretTransform.position, data.range);
            
            if (closestCreep != null) {
                // create projectile
                Projectile.Create(data.projectile, _shootPosition.position, closestCreep);
            }

            yield return Shoot();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.range);
        }
    }
}