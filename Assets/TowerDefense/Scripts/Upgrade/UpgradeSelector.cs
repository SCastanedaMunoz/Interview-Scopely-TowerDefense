using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TowerDefense.Upgrades
{
    public class UpgradeSelector : MonoBehaviour
    {
        private Button _selectorButton;

        private Text _priceText;

        private void Awake()
        {
            _selectorButton = GetComponent<Button>();
            _priceText = GetComponentInChildren<Text>();
        }

        public void Setup(UpgradeHandler.Upgrade upgrade, UnityAction callToUpgrade)
        {
            var image = _selectorButton.targetGraphic as Image;
            if (image != null)
                image.sprite = upgrade.sprite;

            _priceText.text = upgrade.price.ToString();
            
            _selectorButton.onClick.AddListener(callToUpgrade);
        }
    }
}