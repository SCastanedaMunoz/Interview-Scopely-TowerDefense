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

        public Transform SelectorsHolder;
        
        public List<TurretHandlerData.TurretSelection> availableTurrets;

        private void Awake() {
            availableTurrets = data.allSelections;
        }

        private void Start() {
            foreach (var t in availableTurrets) {
                var selector = Instantiate(turretSelectorPrefab, SelectorsHolder);
                selector.Setup(t);
            }
        }

    }
}