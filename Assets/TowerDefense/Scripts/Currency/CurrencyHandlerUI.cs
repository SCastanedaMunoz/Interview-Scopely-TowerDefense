using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Currency
{
    /// <summary>
    /// Currency Handler UI handler
    /// </summary>
    public class CurrencyHandlerUI : MonoBehaviour
    {
        private Text _goldText;

        private void Awake() {
            _goldText = GetComponentInChildren<Text>();
            // setup our gold listener
            CurrencyHandler.Instance.onGoldChanged.AddListener(OnGoldChanged);            
        }

        /// <summary>
        /// update UI whenever gold changes
        /// </summary>
        /// <param name="currentGold"></param>
        private void OnGoldChanged(float currentGold) {
            _goldText.text = $"GOLD: {currentGold}";
        }
    }
}