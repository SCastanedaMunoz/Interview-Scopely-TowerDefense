using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public enum ProjectileType
    {
        Base,
        Fire,
        Ice,
        ForceField
    }
    
    [CreateAssetMenu(fileName = "Base Projectile", menuName = "TowerDefense/Turrets/Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [Min(1f)]
        public float damage = 5f;

        [Min(0.1f)]
        public float speed = 3f;

        [Min(0.1f)]
        public float destroyRadius = .5f;

        public ProjectileType type = ProjectileType.Base;
    }
}