using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Spawers
{
    [CreateAssetMenu(fileName = "Base Spawner", menuName = "TowerDefense/Spawner Data")]
    public class SpawnerData : ScriptableObject
    {
        public float spawnRate = 0.5f;
        
        public List<CreepSpawn> spawnWaves;
    }

    [Serializable]
    public class CreepSpawn
    {
        public string name;
        public List<Creep.Creep> creeps;
        public int spawnCount;
        public float spawnRate;
        public float spawnDelay;
    }
}