using System;
using System.Collections.Generic;
using TowerDefense.Turrets;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// turret data for UI
    /// </summary>
    [CreateAssetMenu(fileName = "All Turrets", menuName = "TowerDefense/Turrets/Turret Handler")]
    public class TurretHandlerData : ScriptableObject
    {
        /// <summary>
        /// contains all different turret selections for UI
        /// </summary>
        public List<TurretSelection> allSelections;
        
        [Serializable]
        public class TurretSelection {
            /// <summary>
            /// identifier
            /// </summary>
            public string name = "Base Turret";

            /// <summary>
            /// turret prefab
            /// </summary>
            public Turret turret;

            /// <summary>
            /// UI sprite
            /// </summary>
            public Sprite sprite;

            /// <summary>
            /// purchase price
            /// </summary>
            public int price = 0;
        }
    }
}