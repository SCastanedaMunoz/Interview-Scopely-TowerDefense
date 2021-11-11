using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Spawers
{
    [CreateAssetMenu(fileName = "Base Spawner", menuName = "TowerDefense/Spawner Data")]
    public class SpawnerData : ScriptableObject
    {
        public List<CreepSpawn> spawnWaves;
    }

    [Serializable]
    public class CreepSpawn
    {
        [Tooltip("Creep spawn identifier")]
        public string name;
        public List<Creep.Creep> creeps;
        [Header("Quantity of creeps to spawn")]
        [Min(1)]
        public int spawnCount;
        [Header("Probability rate of this spawn to be spawn")]
        [Min(0.1f)]
        public float spawnRate;
        [Header("Spawn delay between units")]
        [Min(0.15f)]
        public float spawnDelay = 1f;
    }
}