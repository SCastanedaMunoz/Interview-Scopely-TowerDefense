using TowerDefense.Creeps;
using UnityEngine;

namespace TowerDefense.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public GameObject visual;
        
        [Min(1)] public float damage = 2.5f;

        [Min(0.1f)] public float speed = 10f;

        [Min(0.1f)] public float destroyRadius = .25f;

        private Vector3 _targetPosition = Vector3.zero;

        private Transform _projectileTransform;

        protected Creep Creep;

        private bool _reachedDestination;

        private void Update()
        {
            if (_reachedDestination)
                return;

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

        private void Setup(Creep creep)
        {
            _projectileTransform = transform;
            Creep = creep;
            _targetPosition = Creep.HitPosition;
            var dir = (_targetPosition - _projectileTransform.position).normalized;
            var z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (z < 0)
                z += 360;
            _projectileTransform.eulerAngles = new Vector3(0, 0, z);
        }

        protected virtual void OnDamage()
        {
            Creep.Damage(damage);
            Destroy(gameObject);
        }

        public static void Create(Projectile projectile, Vector3 spawnPosition, Creep creep)
        {
            var projectileSpawn = Instantiate(projectile, spawnPosition, Quaternion.identity);
            projectileSpawn.Setup(creep);
        }
    }
}