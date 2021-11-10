using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;


namespace TowerDefense.Spawers
{
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public SpawnerData spawnerData;
        
        /// <summary>
        /// 
        /// </summary>
        public Transform spawnPoint;

        private float TotalRate;

        private void Awake()
        {
            spawnerData.spawnWaves.ForEach(x => TotalRate += x.spawnRate);
            StartCoroutine(SelectCreepWave());
        }
        
        private IEnumerator SelectCreepWave() {

            var randomRate = Random.Range(0, TotalRate);
            var currentRate = 0f;

            var waves = spawnerData.spawnWaves;
            CreepSpawn selectedWave = null;

            foreach (var creepSpawn in waves) {
                currentRate += creepSpawn.spawnRate;
                if (!(randomRate <= currentRate))
                    continue;
                selectedWave = creepSpawn;
                break;
            }
            
            if (selectedWave == null)
                yield break;
            
            Debug.Log($"Attempting to Spawn: {selectedWave.name}");

            for (var i = 0; i < selectedWave.spawnCount; i++) {
                var selected = 0;
                
                // todo - account for scenarios with 1 creep type or multiple creep types
                if (selectedWave.creeps.Count > 1)
                    selected = Random.Range(0, selectedWave.creeps.Count);

                var spawn = GameObject.Instantiate(selectedWave.creeps[selected], spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(selectedWave.spawnDelay);
            }

        }
    }
}
