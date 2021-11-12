using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Input;
using UnityEngine;
using Random = System.Random;

namespace TowerDefense.Turrets
{
    public class Turret : MonoBehaviour
    {
        public TurretData data; 
        
        public Transform _shootPosition;
        
        private Transform _turretTransform;

        private void Awake()
        {
            _turretTransform = transform;
        }


        // Update is called once per frame
        private void Update()
        {
            var enemy = Creep.Creep.GetClosestCreep(_turretTransform.position, data.range);
            
            if (enemy != null) {
                // create projectile
                
                Projectile.Create(data.projectile, _shootPosition.position,enemy.CreepTransform.position);
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.range);
        }
    }
}