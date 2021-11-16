using System.Collections;
using TowerDefense.Base;
using UnityEngine;

namespace TowerDefense.Spawers
{
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public SpawnerData data;

        /// <summary>
        /// 
        /// </summary>
        public Transform spawnPoint;

        private float _totalRate;

        private void Start()
        {
            data.spawnWaves.ForEach(x => _totalRate += x.spawnRate);
            StartCoroutine(SelectCreepWave());
        }

        private IEnumerator SelectCreepWave()
        {
            while (true)
            {
                var randomRate = Random.Range(0, _totalRate);
                var currentRate = 0f;

                var waves = data.spawnWaves;
                CreepSpawn selectedWave = null;

                foreach (var creepSpawn in waves)
                {
                    currentRate += creepSpawn.spawnRate;
                    if (!(randomRate <= currentRate))
                        continue;
                    selectedWave = creepSpawn;
                    break;
                }

                if (selectedWave == null)
                    yield break;

                foreach (var creepInstance in selectedWave.creeps)
                {
                    for (var i = 0; i < creepInstance.amount; i++)
                    {
                        yield return new WaitUntil(() => !GameManager.Instance.IsGamePaused);
                        var spawn = Instantiate(creepInstance.creep, spawnPoint.position, Quaternion.identity);
                        spawn.CreepTransform.LookAt(PlayerBase.BaseTransform);
                        yield return new WaitForSeconds(selectedWave.spawnDelay);
                    }
                }

                yield return new WaitForSeconds(data.timeBetweenWaves);
            }
        }
    }
}