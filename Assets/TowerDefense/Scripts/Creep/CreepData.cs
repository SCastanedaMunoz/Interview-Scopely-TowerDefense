using UnityEngine;

namespace TowerDefense.Creeps
{
    /// <summary>
    /// contains creep's base data
    /// </summary>
    [CreateAssetMenu(fileName = "Base Creep", menuName = "TowerDefense/Creep")]
    public class CreepData : ScriptableObject
    {
        /// <summary>
        /// base health parameter for generic creep
        /// </summary>
        [Min(1)]
        public int health = 20;

        /// <summary>
        /// base speed parameter for generic creep
        /// </summary>
        [Range(1, 15)]
        public float speed = 2;

        /// <summary>
        /// reward provided to player on death
        /// </summary>
        [Min(1)]
        public float goldReward = 5;
    }
}