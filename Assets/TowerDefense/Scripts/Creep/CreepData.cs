using UnityEngine;

namespace TowerDefense.Creep
{
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
        /// how likely is this mob from enter frenzy mode 
        /// </summary>
        public float frenzyModifier;
    }
}