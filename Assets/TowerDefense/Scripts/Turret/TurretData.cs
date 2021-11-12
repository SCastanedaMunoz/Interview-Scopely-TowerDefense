using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense.Turrets
{
    [CreateAssetMenu(fileName = "Base Turret", menuName = "TowerDefense/Turrets/Turret")]
    public class TurretData : ScriptableObject
    {
        public float shootRate = 0.5f;
        
        [Min(1)] 
        public float range = 1;

        [Min(1)]
        public float rotationRate = 10f;
        
        public Projectile projectile;
    }
}