///<summary>
/// ---> WaveSpawner <---
/// * Spawn the actual wave
/// </summary>

using ShootBottle;
using System.Collections;
using System.Linq;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private EnemyPool poolManager;

    private int bottlesCount;

    private void Start()
    {
        //waveConfig.AddWaveObjects(5);
    }

    private void Update()
    {
        bool noActiveBottles = poolManager.pooledBottles.All(bottle => !bottle.activeInHierarchy);
    }

    public IEnumerator SpawnWave(GameObject obj)
    {
        bottlesCount = waveConfig.GetBottleCount();
        Debug.Log("Spawned Bottles " + bottlesCount + " " + waveConfig.RandomInterval());

        for (int i = 0; i < bottlesCount; i++)
        {
            GameObject bottle = poolManager.GetPooledBottles(obj);
            if (bottle != null)
            {
                bottle.SetActive(true);
            }
            yield return new WaitForSeconds(waveConfig.RandomInterval());
        }
    }
}
