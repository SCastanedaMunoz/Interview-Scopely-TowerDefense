using System.Collections;
using System.Collections.Generic;
using TowerDefense.Base;
using TowerDefense.Currency;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


namespace TowerDefense.Creeps
{
    /// <summary>
    /// Generic Creep
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class Creep : MonoBehaviour
    {
        /// <summary>
        /// contains a reference to all our current alive creeps
        /// </summary>
        private static readonly List<Creep> AllCreeps = new List<Creep>();

        /// <summary>
        /// indicates creep current status
        /// </summary>
        public bool IsDeath => Health <= 0;

        /// <summary>
        /// indicates creep's current health
        /// </summary>
        public float Health { get; private set; }
        
        /// <summary>
        /// indicates creep's current speed 
        /// </summary>
        public float Speed { get; private set; }
        
        /// <summary>
        /// indicates creep's gold reward
        /// </summary>
        public float GoldReward { get; private set; }
        
        /// <summary>
        /// cached transform of base
        /// </summary>
        public Transform CreepTransform { get; private set; }

        /// <summary>
        /// how high or low the turret should aim for this creep
        /// </summary>
        public Vector3 hitPosition =  new Vector3(0, .5f, 0);

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
        private static readonly int HashFlex = Animator.StringToHash("Flex");

        [Header("Creep Behavior Events")] 
        // fires when the creep has been hit
        public UnityEvent<float> onHit = new UnityEvent<float>();

        // fires when the creep has died
        public UnityEvent onDeath = new UnityEvent();

        private void Awake() {
            // let's cache our components
            CreepTransform = transform;
            _creepAnimator = GetComponentInChildren<Animator>();
            _creepAgent = GetComponent<NavMeshAgent>();
            AllCreeps.Add(this);
            
            // setup base information based on data
            Health = data.health;
            GoldReward = data.goldReward;
            SetSpeed(data.speed);
        }

        private void Start()
        {
            // setup game events
            GameManager.Instance.onGameOver.AddListener(OnGameOver);
            GameManager.Instance.onGamePause.AddListener(PauseCreep);
            // setup destination
            _creepAgent.destination = PlayerBase.BaseTransform.position;
        }
        
        private void Update()
        {
            if (IsDeath)
                return;

            if (!(_creepAgent.remainingDistance < 3)) 
                return;
            StopCreep();
            GameManager.Instance.GameOver(false);
            enabled = false;
        }

        #region CREEP BEHAVIORAL FUNCTIONS
        #region SPEED
        /// <summary>
        /// modifies creep's movement speed temporarily and returns it to its original value
        /// </summary>
        /// <param name="newMultiplier">newSpeed multiplier 0-0.9</param>
        /// <param name="duration">effect duration</param>
        public void ModifySpeed(float newMultiplier, float duration)
        {
            // if we already modify the speed, just reset the duration of the effect :) 
            if (_speedModifierCoroutine != null)
                StopCoroutine(_speedModifierCoroutine);
            _speedModifierCoroutine = StartCoroutine(ModifyAndReturnSpeed(newMultiplier, duration));
        }
        
        /// <summary>
        /// sets creep's speed and returns it to its original value
        /// </summary>
        /// <param name="newMultiplier">newSpeed multiplier 0-0.9</param>
        /// <param name="duration">effect duration</param>
        /// <returns></returns>
        private IEnumerator ModifyAndReturnSpeed(float newMultiplier, float duration)
        {
            var speedData = data.speed;
            SetSpeed(speedData * newMultiplier);
            yield return new WaitForSeconds(duration);
            SetSpeed(speedData);
        }
        
        /// <summary>
        /// modifies creep's movement speed and adjust animations according to value
        /// </summary>
        /// <param name="value">new speed value</param>
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
        #endregion

        #region HEALTH
        /// <summary>
        /// damages creep for an specified ammount
        /// </summary>
        /// <param name="amount">damage dealt</param>
        public void Damage(float amount)
        {
            if (IsDeath)
                return;
            
            Health -= amount;
            onHit.Invoke(Health);
            
            if (!IsDeath)
                return;
            onDeath.Invoke();
            StopCreep();
            _creepAnimator.SetTrigger(HashDie);
            AllCreeps.Remove(this);
            if (_speedModifierCoroutine != null)
                StopCoroutine(_speedModifierCoroutine);
            CurrencyHandler.Instance.GoldGain(GoldReward);
            Destroy(gameObject, 2f);
        }
        #endregion

        #region DETECTION
        /// <summary>
        /// provides closes creep based on a position and a maximum range 
        /// </summary>
        /// <param name="position">position to check</param>
        /// <param name="maxRange">maximum range</param>
        /// <returns></returns>
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
        #endregion
        #endregion
        
        #region GAME EVENTS
        private void OnGameOver(bool isWin)
        {
            enabled = false;
            StopCreep();
            if (!isWin)
                _creepAnimator.SetTrigger(HashFlex);
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
        #endregion
    }
}