using System.Collections.Generic;
using TowerDefense.Turrets;
using UnityEngine;

namespace TowerDefense.UI
{
    public class TurretHandlerUI : MonoBehaviour 
    {
        // all scriptable object data could be better referenced using addressable assets :), this works for now  
        public TurretHandlerData data;

        public TurretSelector turretSelectorPrefab;

        public Transform selectorsHolder;
        
        private void Start() {
            foreach (var t in data.allSelections) {
                var selector = Instantiate(turretSelectorPrefab, selectorsHolder);
                selector.Setup(t);
            }
        }
    }
}