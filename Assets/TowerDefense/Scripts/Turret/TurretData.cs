using UnityEngine;
using TowerDefense.Projectiles;

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

        public float shootRateUpgradeRate = 0.1f;

        public float rangeUpgradeRate = 5;

        public float rotationRateUpgradeRate = 15;
        
        public Projectile projectile;
    }
}