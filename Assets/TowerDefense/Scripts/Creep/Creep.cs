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

        /// <summary>
        /// creep's animator
        /// </summary>
        private Animator _creepAnimator;

        // caching the property index is more efficient than string look up :) 
        private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int DieHash = Animator.StringToHash("Die");

        private void Awake()
        {
            // let's cache our components
            _creepTransform = transform;
            _creepAnimator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            // set initial animation states
            _creepAnimator.SetBool(IsWalkingHash, true);
            _creepAnimator.SetFloat(SpeedHash, data.speed);
        }

        private void Update()
        {
            if (Vector3.Distance(_creepTransform.position, PlayerBase.BaseTransform.position) > 3) {
                _creepAnimator.SetBool(IsWalkingHash, true);
                var step = data.speed * Time.deltaTime;
                _creepTransform.position = Vector3.MoveTowards(_creepTransform.position, PlayerBase.BaseTransform.position, step);
            }

            else
            {
                _creepAnimator.SetBool(IsWalkingHash, false);
            }
        }

        private void CheckHealth()
        {
            // calculate health
            
            // show death
            _creepAnimator.SetTrigger(DieHash);
        }
    }
}