using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Upgrades
{
    public class UpgradeHandlerUI : MonoBehaviour
    {
        public UpgradeSelector selectorPrefab;

        public Transform upgradesHolder;

        private void Start()
        {
            var upgradeHandler = UpgradeHandler.Instance;

            CreateSelectorForUpgrade(upgradeHandler.shootRate, upgradeHandler.UpgradeTurretShootRate);
            CreateSelectorForUpgrade(upgradeHandler.turnSpeed, upgradeHandler.UpgradeTurretTurnSpeed);
            CreateSelectorForUpgrade(upgradeHandler.range, upgradeHandler.UpgradeTurretRange);
        }

        private void CreateSelectorForUpgrade(UpgradeHandler.Upgrade upgrade, UnityAction callToUpgrade)
        {
            var upgradeSelector = Instantiate(selectorPrefab, upgradesHolder);
            upgradeSelector.Setup(upgrade, callToUpgrade);
        }
    }
}