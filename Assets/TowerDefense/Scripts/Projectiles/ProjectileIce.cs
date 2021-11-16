using UnityEngine;

namespace TowerDefense.Projectiles
{
    /// <summary>
    /// simple ice projectile
    /// </summary>
    public class ProjectileIce : Projectile
    {
        /// <summary>
        /// how much should it slow creeps
        /// </summary>
        [Range(0, 1)]
        public float slowMultiplier = 0.5f;

        /// <summary>
        /// slow duration
        /// </summary>
        public float slowDuration = 2f;

        /// <inherithdocs />
        protected override void OnDamage()
        {
            // it is possible our target creep gets killed by another turret, account for that
            if (Creep != null)
            {
                // apply speed modifier
                Creep.ModifySpeed(slowMultiplier, slowDuration);
                Creep.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}