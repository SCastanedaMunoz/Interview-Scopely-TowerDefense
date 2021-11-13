using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Projectiles
{
    public class ProjectileIce : Projectile
    {
        [Range(0, 1)]
        public float slowMultiplier = 0.5f;

        public float slowDuration = 2f;

        protected override void OnDamage() {
            Creep.ModifySpeed(slowMultiplier, slowDuration);
            Creep.Damage(damage);
            Destroy(gameObject, slowDuration);
        }
    }
}