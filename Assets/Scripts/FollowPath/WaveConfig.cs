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

        public int bottleVarience = 3;
        public int bottleCounrVarience = 3;

        int totalBottlesInWave = 3;
        float spawnInterval = .5f;

        public float RandomInterval()
        {
            return spawnInterval + (Random.Range(0, spawnInterval));
        }

        public int GetBottleCount()
        {
            return totalBottlesInWave + (Random.Range(0, bottleVarience));
        }
    }
}
