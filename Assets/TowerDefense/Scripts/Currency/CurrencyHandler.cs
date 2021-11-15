using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Currency
{
    public class CurrencyHandler : MonoBehaviour
    {
        public static CurrencyHandler Instance;

        [SerializeField] private float gold = 100;

        public float Gold => gold;

        public UnityEvent<float> onGoldChanged = new UnityEvent<float>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            onGoldChanged.Invoke(gold);
        }

        public bool CanGoldPurchase(float amount) => Gold >= amount;

        public bool GoldPurchase(float amount)
        {
            if (!CanGoldPurchase(amount))
                return false;
            gold -= amount;
            onGoldChanged.Invoke(gold);
            return true;
        }

        public void GoldGain(float amount)
        {
            gold += amount;
            onGoldChanged.Invoke(gold);
        }
    }
}