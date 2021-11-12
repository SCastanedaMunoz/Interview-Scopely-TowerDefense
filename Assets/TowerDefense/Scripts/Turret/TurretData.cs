using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense.Turrets
{
    [CreateAssetMenu(fileName = "Base Turret", menuName = "TowerDefense/Turrets/Turret")]
    public class TurretData : ScriptableObject
    {
        [Min(1)] 
        public float range = 1;

        [Min(1)]
        public float rotationRate = 10f;
        
        public Projectile projectile;
    }
}