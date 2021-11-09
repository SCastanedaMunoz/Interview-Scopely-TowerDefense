using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense.Creep
{
    public class Creep : MonoBehaviour
    {
        /// <summary>
        /// creep's data
        /// </summary>
        public CreepData data; 
        
        /// <summary>
        /// cached transform of base
        /// </summary>
        private static Transform _creepTransform;

        private void Awake()
        {
            _creepTransform = transform;
        }
    }
}