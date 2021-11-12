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
        [Header("Hover over properties for details")]
        [Tooltip("Creep spawn identifier")]
        public string name;
        public List<Creeps.Creep> creeps;
        [Tooltip("Quantity of creeps to spawn")]
        [Min(1)]
        public int spawnCount;
        [Tooltip("Probability rate of this spawn to be spawn")]
        [Min(0.1f)]
        public float spawnRate;
        [Tooltip("Spawn delay between units")]
        [Min(0.15f)]
        public float spawnDelay = 1f;
    }
}