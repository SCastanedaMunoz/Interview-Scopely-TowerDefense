using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Currency
{
    /// <summary>
    /// handle's currency exchanges. (this is not safe, maybe to server auth, but for prototype is OK :D)
    /// </summary>
    public class CurrencyHandler : MonoBehaviour
    {
        /// <summary>
        /// static reference to Currency Handler. Singletons could have been handled better but for prototype purposes, this is ok
        /// </summary>
        public static CurrencyHandler Instance;

        // initial gold value
        [SerializeField] private float gold = 100;

        /// <summary>
        /// accessor for current gold
        /// </summary>
        public float Gold => gold;

        /// <summary>
        /// fires when gold amount changes
        /// </summary>
        public UnityEvent<float> onGoldChanged = new UnityEvent<float>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // setup anything that needs to know about gold
            onGoldChanged.Invoke(gold);
        }

        /// <summary>
        /// returns whether the purchase can be done
        /// </summary>
        /// <param name="amount">amount to check</param>
        /// <returns></returns>
        public bool CanGoldPurchase(float amount) => Gold >= amount;

        /// <summary>
        /// attempt a gold purchase
        /// </summary>
        /// <param name="amount">amount to spend</param>
        /// <returns>success value of purchase</returns>
        public bool GoldPurchase(float amount)
        {
            if (!CanGoldPurchase(amount))
                return false;
            gold -= amount;
            onGoldChanged.Invoke(gold);
            return true;
        }

        /// <summary>
        /// provide with gold
        /// </summary>
        /// <param name="amount">gold to add</param>
        public void GoldGain(float amount)
        {
            gold += amount;
            onGoldChanged.Invoke(gold);
        }
    }
}