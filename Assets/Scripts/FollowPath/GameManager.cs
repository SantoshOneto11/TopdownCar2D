using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace ShootBottle
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Bottle bottle;
        [SerializeField] private EnemyPool poolManager;
        [SerializeField] private BaseGun gun;
        [SerializeField] private WaveSpawner waveSpawner;

        [SerializeField] private int objectsInWave = 5;
        [SerializeField] private int varience = 3;
        [SerializeField] private int totalWave = 5;
        [SerializeField] private float spawnInterval = 2f;

        private float remainingBottles;

        private void Start()
        {
            StartCoroutine(waveSpawner.SpawnWave(bottle.gameObject));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                gun.Shoot(poolManager.GetPooledBullets());
            }
        }

        public void SendPoolData(GameObject obj)
        {
            poolManager.ReturnToPool(obj);
        }
    }
}
