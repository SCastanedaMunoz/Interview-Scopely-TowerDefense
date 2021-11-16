using UnityEngine;
using TowerDefense.Projectiles;

namespace TowerDefense.Turrets
{
    /// <summary>
    /// turret data
    /// </summary>
    [CreateAssetMenu(fileName = "Base Turret", menuName = "TowerDefense/Turrets/Turret")]
    public class TurretData : ScriptableObject
    {
        /// <summary>
        /// how often the turret shoots
        /// </summary>
        [Tooltip("how often the turret shoots")]
        public float shootRate = 0.5f;
        
        /// <summary>
        /// turret attack range
        /// </summary>
        [Tooltip("turret attack range")]
        [Min(1)] 
        public float range = 1;

        /// <summary>
        /// how fast the turret rotates
        /// </summary>
        [Tooltip("how fast the turret rotates")]
        [Min(1)]
        public float rotationRate = 10f;

        /// <summary>
        /// how much shoot rate get's decreased on upgrade
        /// </summary>
        [Tooltip("how much shoot rate get's decreased on upgrade")]
        public float shootRateUpgradeRate = 0.1f;

        /// <summary>
        /// how much shoot range increases on upgrade
        /// </summary>
        [Tooltip("how much shoot range increases on upgrade")]
        public float rangeUpgradeRate = 5;

        /// <summary>
        /// how much rotation rate increases on upgrade
        /// </summary>
        [Tooltip("how much rotation rate increases on upgrade")]
        public float rotationRateUpgradeRate = 15;
        
        /// <summary>
        /// turret's projectile
        /// </summary>
        public Projectile projectile;
    }
}