using System.Collections;
using UnityEngine;

namespace TowerDefense.Projectiles {
    
    /// <summary>
    /// simple fire projectile
    /// </summary>
    public class ProjectileFire : Projectile {
        
        /// <summary>
        /// how often should fire damage be applied
        /// </summary>
        public float fireTickDelay = 0.5f;
        
        
        /// <summary>
        /// how many times should fire be applied
        /// </summary>
        public int fireTickSteps = 5;

        private Coroutine _fireDmgCoroutine;

        /// <inherithdocs />
        protected override void OnGameOver(bool isWin)
        {
            base.OnGameOver(isWin);
            if (_fireDmgCoroutine != null)
                StopCoroutine(ApplyFireDamage());
        }

        /// <inherithdocs />
        protected override void OnDamage() {
            _fireDmgCoroutine = StartCoroutine(ApplyFireDamage());
        }

        /// <summary>
        /// simple fire damage logic
        /// </summary>
        /// <returns></returns>
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