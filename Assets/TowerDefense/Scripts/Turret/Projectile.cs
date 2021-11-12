using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Turrets
{
    public class Projectile : MonoBehaviour
    {
        public ProjectileData data;

        private Transform _projectileTransform;
        
        private Vector3 _targetPosition;


        private void Awake()
        {
            _projectileTransform = transform;
        }

        private void Update()
        {
            var moveDir = (_targetPosition - _projectileTransform.position).normalized;
            _projectileTransform.position += moveDir * data.speed * Time.deltaTime;

            // if (Vector3.Distance(_projectileTransform.position, _targetPosition) < data.destroyRadius) {
            //     Debug.Log("Destroying Projectile After Reaching Position");
            //     Destroy(gameObject);
            // }
        }
        
        public static void Create(Projectile projectile,  Vector3 spawnPosition, Vector3 targetPosition)
        {
            
            Debug.LogError($"Spawn Position: {spawnPosition}, and Target Position: {targetPosition}");
            var arrowTransform = Instantiate(projectile, spawnPosition, Quaternion.identity);
            projectile._targetPosition = targetPosition;
        }
    }
}