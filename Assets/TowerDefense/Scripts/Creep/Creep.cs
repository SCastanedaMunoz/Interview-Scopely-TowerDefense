using System.Collections;
using System.Collections.Generic;
using TowerDefense.Base;
using TowerDefense.Currency;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


namespace TowerDefense.Creeps
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Creep : MonoBehaviour
    {
        private static readonly List<Creep> AllCreeps = new List<Creep>();

        public bool IsDeath => Health <= 0;

        public float Health { get; private set; }
        public float Speed { get; private set; }
        public float GoldReward { get; private set; }
        
        /// <summary>
        /// cached transform of base
        /// </summary>
        public Transform CreepTransform { get; private set; }

        public Vector3 HitPosition => CreepTransform.position + new Vector3(0, .5f, 0);

        /// <summary>
        /// creep's data
        /// </summary>
        public CreepData data;

        /// <summary>
        /// creep's animator
        /// </summary>
        private Animator _creepAnimator;

        /// <summary>
        /// creep's Nav Mesh Agent
        /// </summary>
        private NavMeshAgent _creepAgent;
        
        private Coroutine _speedModifierCoroutine;

        // caching the property index is more efficient than string look up :) 
        private static readonly int HashIsWalking = Animator.StringToHash("isWalking");
        private static readonly int HasSpeed = Animator.StringToHash("Speed");
        private static readonly int HashDie = Animator.StringToHash("Die");

        [Header("Creep Behavior Events")] 
        public UnityEvent onSpawn = new UnityEvent();

        public UnityEvent<float> onHit = new UnityEvent<float>();

        public UnityEvent onDeath = new UnityEvent();

        private void Awake()
        {
            // let's cache our components
            CreepTransform = transform;
            _creepAnimator = GetComponentInChildren<Animator>();
            _creepAgent = GetComponent<NavMeshAgent>();
            AllCreeps.Add(this);
            Health = data.health;
            GoldReward = data.goldReward;
            SetSpeed(data.speed);
        }

        private void Start()
        {
            // set initial animation states
            GameManager.Instance.onGameOver.AddListener(OnGameOver);
            GameManager.Instance.onGamePause.AddListener(PauseCreep);
            _creepAgent.destination = PlayerBase.BaseTransform.position;
            onSpawn.Invoke();
        }

        private void OnGameOver(bool isWin)
        {
            StopCreep();
            GameManager.Instance.GameOver(false);
        }

        private void StopCreep()
        {
            _creepAgent.isStopped = true;
            _creepAnimator.SetBool(HashIsWalking, false);
        }

        private void PauseCreep(bool isPaused = false) {
            _creepAgent.isStopped = isPaused;
            _creepAnimator.enabled = !isPaused;
        }

        private void Update()
        {
            if (!IsDeath && !(_creepAgent.remainingDistance < 3))
                return;
            StopCreep();
        }
        
        private void SetSpeed(float value)
        {
            Speed = value;
            if (value <= 0)
            {
                _creepAnimator.SetBool(HashIsWalking, false);
            }
            else
            {
                _creepAnimator.SetFloat(HasSpeed, Speed);
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
            if (_speedModifierCoroutine != null)
                StopCoroutine(_speedModifierCoroutine);
            _speedModifierCoroutine = StartCoroutine(ModifyAndReturnSpeed(newMultiplier, duration));
        }

        public void Damage(float amount)
        {
            if (IsDeath)
                return;
            
            Health -= amount;
            onHit.Invoke(Health);
            
            if (!IsDeath)
                return;
            onDeath.Invoke();
            _creepAnimator.SetTrigger(HashDie);
            AllCreeps.Remove(this);
            if (_speedModifierCoroutine != null)
                StopCoroutine(_speedModifierCoroutine);
            CurrencyHandler.Instance.GoldGain(GoldReward);
            Destroy(gameObject, 2f);
        }

        public static Creep GetClosestCreep(Vector3 position, float maxRange)
        {
            Creep closestCreep = null;

            foreach (var creep in AllCreeps)
            {
                if (creep.IsDeath)
                    continue;

                if (!(Vector3.Distance(position, creep.CreepTransform.position) <= maxRange))
                    continue;

                if (closestCreep == null)
                    closestCreep = creep;
                else
                {
                    if (Vector3.Distance(position, creep.CreepTransform.position) <
                        Vector3.Distance(position, closestCreep.CreepTransform.position))
                    {
                        closestCreep = creep;
                    }
                }
            }

            return closestCreep;
        }
    }
}