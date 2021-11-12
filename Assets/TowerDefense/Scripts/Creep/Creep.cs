using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Base;
using UnityEngine;


namespace TowerDefense.Creep
{
    public class Creep : MonoBehaviour
    {
        public static readonly List<Creep> AllCreeps = new List<Creep>();

        public Transform CreepTransform => _creepTransform;
        
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
        private static readonly int HashIsWalking = Animator.StringToHash("isWalking");
        private static readonly int HasSpeed = Animator.StringToHash("Speed");
        private static readonly int HashDie = Animator.StringToHash("Die");

        private void Awake() {
            // let's cache our components
            _creepTransform = transform;
            _creepAnimator = GetComponentInChildren<Animator>();
            AllCreeps.Add(this);
        }

        private void Start() {
            // set initial animation states
            _creepAnimator.SetBool(HashIsWalking, true);
            _creepAnimator.SetFloat(HasSpeed, data.speed);
        }

        private void Update() {
            if (Vector3.Distance(_creepTransform.position, PlayerBase.BaseTransform.position) > 3)
            {
                _creepAnimator.SetBool(HashIsWalking, true);
                var step = data.speed * Time.deltaTime;
                _creepTransform.position =
                    Vector3.MoveTowards(_creepTransform.position, PlayerBase.BaseTransform.position, step);
            }

            else
            {
                _creepAnimator.SetBool(HashIsWalking, false);
            }
        }

        private void CheckHealth()
        {
            // calculate health

            // show death
            _creepAnimator.SetTrigger(HashDie);
        }

        public static Creep GetClosestCreep(Vector3 position, float maxRange) {
            Creep closestCreep = null;

            foreach (var creep in AllCreeps) {
                //todo - account for death enemies

                if (!(Vector3.Distance(position, creep._creepTransform.position) <= maxRange)) 
                    continue;
                
                if (closestCreep == null)
                {
                    closestCreep = creep;
                }
                else
                {
                    if (Vector3.Distance(position, creep._creepTransform.position) <
                        Vector3.Distance(position, closestCreep._creepTransform.position))
                    {
                        closestCreep = creep;
                    }
                }
            }

            return closestCreep;
        }
    }
}
