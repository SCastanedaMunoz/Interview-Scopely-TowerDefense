using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Creeps {
    public class HealthViewer : MonoBehaviour {
        public Image healthBar;
        
        private float _healthMax;

        private Creep _creep;

        private void Start()
        {
            _creep = GetComponentInParent<Creep>();
            _healthMax = _creep.Health;
            _creep.onHit.AddListener(OnCreepHit);
            _creep.onDeath.AddListener(OnCreepDeath);
        }

        private void OnCreepDeath()
        {
            gameObject.SetActive(false);
        }

        private void OnCreepHit(float currentHeath)
        {
            healthBar.fillAmount = currentHeath / _healthMax;
        }
    }
}