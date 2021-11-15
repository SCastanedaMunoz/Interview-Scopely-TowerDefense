using System;
using System.Collections.Generic;
using TowerDefense.Turrets;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "All Turrets", menuName = "TowerDefense/Turrets/Turret Handler")]
    public class TurretHandlerData : ScriptableObject
    {
        public List<TurretSelection> allSelections;
        
        [Serializable]
        public class TurretSelection {
            public string name = "Base Turret";

            public Turret turret;

            public Sprite sprite;

            public int price = 0;
        }
    }
}