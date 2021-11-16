using System;
using TowerDefense.Currency;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Upgrades
{
    public class UpgradeHandler : MonoBehaviour
    {
        public static UpgradeHandler Instance;
        
        // these could also be moved into scriptable objects, however, it would be better if upgrades can be fetched in a dictionary style system
        // for not an demonstration purposes, I will leave it like this :) but it should be similar to the turrets or creeps.
        [Header("Turret Upgrade Variables")] 
        public Upgrade shootRate;

        public Upgrade turnSpeed;

        public Upgrade range;

        private void Awake()
        {
            Instance = this;
        }

        public void UpgradeTurretShootRate()
        {
            AttemptUpgrade(shootRate);
        }

        public void UpgradeTurretTurnSpeed()
        {
            AttemptUpgrade(turnSpeed);
        }

        public void UpgradeTurretRange()
        {
            AttemptUpgrade(range);
        }

        private void AttemptUpgrade(Upgrade upgrade)
        {
            if (!CurrencyHandler.Instance.GoldPurchase(upgrade.price)) 
                return;
            upgrade.onUpgrade.Invoke();
            upgrade.TimesUpgraded = upgrade.TimesUpgraded + 1;
            // todo - upgrades cost should increase every time! but not for now :(
        }

        // todo - make generic or similar, we want to support more than just int
        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        public class Upgrade
        {
            public Sprite sprite;
            
            public int price;
            public int TimesUpgraded { get; set; }

            public UnityEvent onUpgrade;
        }

    }
}