using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Creeps {
    
    // Could have used something else, but this works for prototype purposes, also it does not face directly to camera :(
    
    /// <summary>
    /// simple health viewer for creeps
    /// </summary>
    public class HealthViewer : MonoBehaviour {
        
        /// <summary>
        /// image displaying creep's health
        /// </summary>
        public Image healthBar;
        
        /// <summary>
        /// creep's max health value
        /// </summary>
        private float _healthMax;

        /// <summary>
        /// target creep for health viewer
        /// </summary>
        private Creep _creep;

        private void Start()
        {
            _creep = GetComponentInParent<Creep>();
            _healthMax = _creep.Health;
            _creep.onHit.AddListener(OnCreepHit);
            _creep.onDeath.AddListener(OnCreepDeath);
        }

        /// <summary>
        /// triggered on creep's death
        /// </summary>
        private void OnCreepDeath()
        {
            // disable viewer
            gameObject.SetActive(false);
        }

        /// <summary>
        /// trigger on creep's hit
        /// </summary>
        /// <param name="currentHeath">creep's current health</param>
        private void OnCreepHit(float currentHeath)
        {
            // adjust fill amount
            healthBar.fillAmount = currentHeath / _healthMax;
        }
    }
}