using System.Collections;
using System.Collections.Generic;
using TowerDefense.Base;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


namespace TowerDefense.Creeps
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Creep : MonoBehaviour
    {
        public static readonly List<Creep> AllCreeps = new List<Creep>();

        public Transform CreepTransform => _creepTransform;

        public Vector3 HitPosition => _creepTransform.position + new Vector3(0, .5f, 0);

        public bool IsDeath => health <= 0;

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

        /// <summary>
        /// creep's Nav Mesh Agent
        /// </summary>
        private NavMeshAgent _creepAgent;
        
        
        private Coroutine speedModifierCoroutine;


        public float health { get; private set; }
        public float speed { get; private set; }
        public float frenzyModifier { get; private set; }

        // caching the property index is more efficient than string look up :) 
        private static readonly int HashIsWalking = Animator.StringToHash("isWalking");
        private static readonly int HasSpeed = Animator.StringToHash("Speed");
        private static readonly int HashDie = Animator.StringToHash("Die");

        [Header("Creep Behavior Events")] 
        public UnityEvent onSpawn = new UnityEvent();

        public UnityEvent<float> onHit = new UnityEvent<float>();

        public UnityEvent onFrenzy = new UnityEvent();

        public UnityEvent onDeath = new UnityEvent();

        private void Awake()
        {
            // let's cache our components
            _creepTransform = transform;
            _creepAnimator = GetComponentInChildren<Animator>();
            _creepAgent = GetComponent<NavMeshAgent>();
            AllCreeps.Add(this);

            health = data.health;
            frenzyModifier = data.frenzyModifier;
            SetSpeed(data.speed);
        }

        private void Start()
        {
            // set initial animation states
            _creepAgent.destination = PlayerBase.BaseTransform.position;
        }

        private void Update()
        {
            if (!IsDeath && !(_creepAgent.remainingDistance < 3))
                return;
            _creepAgent.isStopped = true;
            _creepAnimator.SetBool(HashIsWalking, false);
            onSpawn.Invoke();
        }
        
        private void SetSpeed(float value)
        {
            speed = value;
            if (value <= 0)
            {
                _creepAnimator.SetBool(HashIsWalking, false);
            }
            else
            {
                _creepAnimator.SetFloat(HasSpeed, speed);
                _creepAnimator.SetBool(HashIsWalking, true);
            }
            if (_creepAgent != null)
                _creepAgent.speed = value;
        }
        
        private IEnumerator ModifyAndReturnSpeed(float newMultiplier, float duration)
        {
            var speedData = data.speed;
            SetSpeed(speedData * newMultiplier);
            yield return new WaitForSeconds(duration);
            SetSpeed(speedData);
        }
        
        public void ModifySpeed(float newMultiplier, float duration)
        {
            // if we already modify the speed, just reset the duration of the effect :) 
            if (speedModifierCoroutine != null)
                StopCoroutine(speedModifierCoroutine);
            speedModifierCoroutine = StartCoroutine(ModifyAndReturnSpeed(newMultiplier, duration));
        }

        public void Damage(float amount)
        {
            health -= amount;
            onHit.Invoke(health);
            
            if (!IsDeath)
                return;
            onDeath.Invoke();
            _creepAnimator.SetTrigger(HashDie);
        }

        public static Creep GetClosestCreep(Vector3 position, float maxRange)
        {
            Creep closestCreep = null;

            foreach (var creep in AllCreeps)
            {
                if (creep.IsDeath)
                    continue;

                if (!(Vector3.Distance(position, creep._creepTransform.position) <= maxRange))
                    continue;

                if (closestCreep == null)
                    closestCreep = creep;
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