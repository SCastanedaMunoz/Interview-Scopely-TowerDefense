using TowerDefense.Creeps;
using UnityEngine;

namespace TowerDefense.Projectiles
{
    /// <summary>
    /// base projectile class all projectiles should inherit
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// visual representation of projectile
        /// </summary>
        public GameObject visual;

        /// <summary>
        /// projectile damage to creeps
        /// </summary>
        [Min(1)] public float damage = 2.5f;

        /// <summary>
        /// projectile speed
        /// </summary>
        [Min(0.1f)] public float speed = 10f;

        /// <summary>
        /// projectile destruction radius
        /// </summary>
        // I WANTED TO DO BETTER CHECKS FOR HITS AND DESTRUCTION BUT NO TIME....
        [Min(0.1f)] public float destroyRadius = .25f;

        /// <summary>
        /// projectile's target position
        /// </summary>
        private Vector3 _targetPosition = Vector3.zero;

        /// <summary>
        /// cached projectile's transform
        /// </summary>
        private Transform _projectileTransform;

        /// <summary>
        /// target creep
        /// </summary>
        protected Creep Creep;

        /// <summary>
        /// indicates if projectile reached destination
        /// </summary>
        private bool _reachedDestination;

        #region GAME EVENTS

        private void Start()
        {
            GameManager.Instance.onGamePause.AddListener(OnGamePause);
            GameManager.Instance.onGameOver.AddListener(OnGameOver);
        }

        protected virtual void OnGamePause(bool isPaused)
        {
            _reachedDestination = isPaused;
        }

        protected virtual void OnGameOver(bool isWin)
        {
            _reachedDestination = true;
        }

        private void Update()
        {
            if (_reachedDestination)
                return;
            OnMovement();
        }

        #endregion

        /// <summary>
        /// set's up a projectile based on a target creep
        /// </summary>
        /// <param name="creep"></param>
        private void Setup(Creep creep)
        {
            _projectileTransform = transform;
            Creep = creep;
            _targetPosition = Creep.CreepTransform.position + Creep.hitPosition;
            var dir = (_targetPosition - _projectileTransform.position).normalized;
            var z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (z < 0)
                z += 360;
            _projectileTransform.eulerAngles = new Vector3(0, 0, z);
        }

        /// <summary>
        /// dictates how movement should be calculated for projectiles
        /// </summary>
        protected virtual void OnMovement()
        {
            var position = _projectileTransform.position;
            var moveDir = (_targetPosition - position).normalized;
            position += moveDir * speed * Time.deltaTime;
            _projectileTransform.position = position;

            // todo - this does not necessarily mean we hit our target, it just means we reached our destination, account for actually hitting target?
            if (!(Vector3.Distance(_projectileTransform.position, _targetPosition) < destroyRadius))
                return;
            _reachedDestination = true;
            visual.SetActive(false);
            OnDamage();
        }

        /// <summary>
        /// indicates how damage should be calculated for creeps
        /// </summary>
        protected virtual void OnDamage()
        {
            if (Creep != null)
                Creep.Damage(damage);
            Destroy(gameObject);
        }

        /// <summary>
        /// easy create :)
        /// </summary>
        /// <param name="projectile">prefab</param>
        /// <param name="spawnPosition">spawn position</param>
        /// <param name="creep">target creep</param>
        public static void Create(Projectile projectile, Vector3 spawnPosition, Creep creep)
        {
            var projectileSpawn = Instantiate(projectile, spawnPosition, Quaternion.identity);
            projectileSpawn.Setup(creep);
        }
    }
}