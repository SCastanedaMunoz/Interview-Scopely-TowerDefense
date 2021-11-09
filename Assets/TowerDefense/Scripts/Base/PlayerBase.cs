using UnityEngine;

namespace TowerDefense.Base
{
    public class PlayerBase : MonoBehaviour
    {
        /// <summary>
        /// accessor for spawnable creeps
        /// </summary>
        public static Transform BaseTransform => _baseTransform;

        /// <summary>
        /// cached transform of base
        /// </summary>
        private static Transform _baseTransform;

        private void Awake()
        {
            _baseTransform = transform;
        }
    }
}