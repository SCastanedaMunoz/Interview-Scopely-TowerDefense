using System.Collections;
using TowerDefense.Base;
using UnityEngine;

namespace TowerDefense.Spawers
{
    /// <summary>
    /// simple spawner
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// spawner's data
        /// </summary>
        public SpawnerData data;

        /// <summary>
        /// where creep's spawn from
        /// </summary>
        public Transform spawnPoint;

        /// <summary>
        /// total % rate of waves
        /// </summary>
        private float _totalRate;

        private Coroutine _spawnCoroutine;

        private void Start()
        {
            data.spawnWaves.ForEach(x => _totalRate += x.spawnRate);
            _spawnCoroutine = StartCoroutine(SelectCreepWave());
            GameManager.Instance.onGameOver.AddListener(OnGameOver);
        }

        private void OnGameOver(bool isWin)
        {
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        /// <summary>
        /// selects a wave to spawn
        /// </summary>
        /// <returns></returns>
        private IEnumerator SelectCreepWave()
        {
            while (true)
            {
                // get a random rate
                var randomRate = Random.Range(0, _totalRate);
                var currentRate = 0f;

                var waves = data.spawnWaves;
                CreepSpawn selectedWave = null;

                foreach (var creepSpawn in waves)
                {
                    currentRate += creepSpawn.spawnRate;
                    if (!(randomRate <= currentRate))
                        continue;
                    // once wave is obtained, break
                    selectedWave = creepSpawn;
                    break;
                }

                // if something goes wrong...
                if (selectedWave == null)
                    yield break;

                // spawn all creep types inside wave
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