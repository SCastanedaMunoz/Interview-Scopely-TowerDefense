using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Creeps;
using UnityEngine;

namespace TowerDefense.Turrets
{
    public class Projectile : MonoBehaviour
    {
        public ProjectileData data;

        private Vector3 _targetPosition = Vector3.zero;

        private Transform _projectileTransform;

        private Creep _creep;

        private void Update() {
            var position = _projectileTransform.position;
            var moveDir = (_targetPosition - position).normalized;
            position += moveDir * data.speed * Time.deltaTime;
            _projectileTransform.position = position;

            if (!(Vector3.Distance(_projectileTransform.position, _targetPosition) < data.destroyRadius)) 
                return;
            _creep.Damage(data.damage);
            Destroy(gameObject);
        }

        private void Setup(Creep creep) {
            _projectileTransform = transform;
            _creep = creep;
            _targetPosition = _creep.CreepTransform.position;
            var dir = (_targetPosition - _projectileTransform.position).normalized;
            var z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (z < 0)
                z += 360;
            _projectileTransform.eulerAngles = new Vector3(0, 0, z);
        }

        public static void Create(Projectile projectile,  Vector3 spawnPosition, Creep creep) {
            var projectileSpawn = Instantiate(projectile, spawnPosition, Quaternion.identity);
            projectileSpawn.Setup(creep);
        }
    }
}