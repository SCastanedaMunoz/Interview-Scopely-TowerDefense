using UnityEngine;

namespace TowerDefense.Projectiles
{
    public class ProjectileIce : Projectile
    {
        [Range(0, 1)]
        public float slowMultiplier = 0.5f;

        public float slowDuration = 2f;

        protected override void OnDamage() {
            // it is possible our target creep gets killed by another turret, account for that
            if (Creep != null) {
                Creep.ModifySpeed(slowMultiplier, slowDuration);
                Creep.Damage(damage);
            }
            Destroy(gameObject);
        }
    }
}