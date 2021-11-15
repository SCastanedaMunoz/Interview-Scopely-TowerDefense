using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TowerDefense.Currency
{
    public class CurrencyHandlerUI : MonoBehaviour
    {
        private Text _goldText;

        private void Awake() {
            _goldText = GetComponentInChildren<Text>();
            CurrencyHandler.Instance.onGoldChanged.AddListener(OnGoldChanged);            
        }

        private void OnGoldChanged(float currentGold) {
            _goldText.text = $"GOLD: {currentGold}";
        }
    }
}