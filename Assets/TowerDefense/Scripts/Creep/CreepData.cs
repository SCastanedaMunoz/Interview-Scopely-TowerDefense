using UnityEngine;

namespace TowerDefense.Creep
{
    [CreateAssetMenu(fileName = "Base Creep", menuName = "TowerDefense/Creep")]
    public class CreepData : ScriptableObject
    {
        /// <summary>
        /// base health parameter for generic creep
        /// </summary>
        public int health;

        /// <summary>
        /// base speed parameter for generic creep
        /// </summary>
        public float speed;

        /// <summary>
        /// how likely is this mob from enter frenzy mode 
        /// </summary>
        public float frenzyModifier;
    }
}