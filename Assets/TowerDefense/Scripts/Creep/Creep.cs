using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Base;
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
        private Transform _creepTransform;

        private bool shouldMove = true;

        private void Awake()
        {
            _creepTransform = transform;
        }

        private void Update()
        {
            var step = data.speed * Time.deltaTime;
            _creepTransform.position =
                Vector3.MoveTowards(_creepTransform.position, PlayerBase.BaseTransform.position, step);
        }
    }
}