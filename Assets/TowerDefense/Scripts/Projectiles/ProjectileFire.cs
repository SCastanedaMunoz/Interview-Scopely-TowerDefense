using System.Collections;
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
            for (var i = 0; i < fireTickSteps; i++) {
                yield return new WaitUntil(() => !GameManager.Instance.IsGamePaused);
                if (Creep != null)
                {
                    Creep.Damage(damage);
                    yield return new WaitForSeconds(fireTickDelay);
                }
                else
                {
                    Destroy(gameObject);
                    yield break;
                }

            }
            Destroy(gameObject);
        }
    }
}