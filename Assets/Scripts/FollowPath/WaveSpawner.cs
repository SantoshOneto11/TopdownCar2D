///<summary>
/// ---> WaveSpawner <---
/// * Spawn the actual wave
/// </summary>

using ShootBottle;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private EnemyPool poolManager;
    [SerializeField] private GameManager gameManager;

    private int bottlesCount;
    public UnityEvent<int> OnTotalBottlesCount;
    private void Start()
    {
        OnTotalBottlesCount.AddListener(gameManager.GetNewBottlesCount);
    }

    private void Update()
    {
        bool noActiveBottles = poolManager.pooledBottles.All(bottle => !bottle.activeInHierarchy);
    }

    public IEnumerator SpawnWave(GameObject obj)
    {
        bottlesCount = waveConfig.GetBottleCount();
        OnTotalBottlesCount.Invoke(bottlesCount);

        float spawnInterval = waveConfig.RandomInterval();
        for (int i = 0; i < bottlesCount; i++)
        {
            GameObject bottle = poolManager.GetPooledBottles(obj);
            if (bottle != null)
            {
                bottle.SetActive(true);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
