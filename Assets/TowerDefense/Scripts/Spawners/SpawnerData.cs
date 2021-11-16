using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Creeps;
using UnityEngine;

namespace TowerDefense.Spawers
{
    [CreateAssetMenu(fileName = "Base Spawner", menuName = "TowerDefense/Spawner Data")]
    public class SpawnerData : ScriptableObject
    {
        public float timeBetweenWaves = 4f;
        
        public List<CreepSpawn> spawnWaves;
    }

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

    [Serializable]
    public class CreepInstance
    {
        public Creep creep;
        [Min(1)]
        public int amount = 1;
    }
}