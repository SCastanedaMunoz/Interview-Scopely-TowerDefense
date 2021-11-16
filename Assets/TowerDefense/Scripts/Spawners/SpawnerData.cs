using System;
using System.Collections.Generic;
using TowerDefense.Creeps;
using UnityEngine;

namespace TowerDefense.Spawers
{
    /// <summary>
    /// simple spawner data
    /// </summary>
    [CreateAssetMenu(fileName = "Base Spawner", menuName = "TowerDefense/Spawner Data")]
    public class SpawnerData : ScriptableObject
    {
        /// <summary>
        /// delay between waves
        /// </summary>
        public float timeBetweenWaves = 4f;
        
        /// <summary>
        /// spawner's waves information
        /// </summary>
        public List<CreepSpawn> spawnWaves;
    }

    /// <summary>
    /// contains information about a wave
    /// </summary>
    [Serializable]
    public class CreepSpawn
    {
        [Header("Hover over properties for details")]
        [Tooltip("Creep spawn identifier")]
        public string name;
        public List<CreepInstance> creeps = new List<CreepInstance>();
        [Tooltip("Probability rate of this spawn to be spawn")]
        [Min(0.1f)]
        public float spawnRate;
        [Tooltip("Spawn delay between units")]
        [Min(0.15f)]
        public float spawnDelay = 1f;
    }

    /// <summary>
    /// utilized to specify creep amount inside waves.
    /// </summary>
    [Serializable]
    public class CreepInstance
    {
        public Creep creep;
        [Min(1)]
        public int amount = 1;
    }
}