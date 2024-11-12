///<summary>
/// ---> WaveConfig <---
/// * Calculate RandomInterval
/// * Calculate RandomBottle Count to Spawn
/// </summary>
using UnityEngine;

namespace ShootBottle
{
    [CreateAssetMenu(menuName = "CustomData/WaveConfig", order = 2)]
    public class WaveConfig : ScriptableObject
    {
        public Bottle bottlePrefab;

        int bottleVarience = 1;
        int totalBottlesInWave = 3;
        [Range(.3f, 1f)]
        [SerializeField] public float spawnInterval = .5f;

        public float RandomInterval()
        {
            float spawnTime = spawnInterval + (Random.Range(0.1f, spawnInterval));
            Debug.Log("SpawnTime - " + spawnTime);
            return spawnTime;
        }

        public int GetBottleCount()
        {
            //int temp = 
            //bottleVarience++;

            //return totalBottlesInWave + (Random.Range(0, bottleVarience));
            return 10;
        }

    }
}
