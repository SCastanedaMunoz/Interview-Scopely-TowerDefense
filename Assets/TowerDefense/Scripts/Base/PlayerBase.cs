using UnityEngine;

namespace TowerDefense.Base
{
    /// <summary>
    /// simple player base, reference for creeps
    /// </summary>
    public class PlayerBase : MonoBehaviour
    {
        /// <summary>
        /// accessor for spawnable creeps
        /// </summary>
        public static Transform BaseTransform { get; private set; }

        private void Awake()
        {
            // cache player base due to high reference and usage
            BaseTransform = transform;
        }
    }
}