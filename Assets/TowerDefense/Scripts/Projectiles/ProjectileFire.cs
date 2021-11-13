using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefense.Projectiles {
    public class ProjectileFire : Projectile {
        public float fireTickDelay = 0.5f;

        public int fireTickSteps = 5;
        
        protected override void OnDamage() {
            StartCoroutine(ApplyFireDamage());
        }

        private IEnumerator ApplyFireDamage()
        {
            for (var i = 0; i < fireTickSteps; i++)
            {
                Creep.Damage(damage);
                yield return new WaitForSeconds(fireTickDelay);
            }
            Destroy(gameObject);
        }
    }
}