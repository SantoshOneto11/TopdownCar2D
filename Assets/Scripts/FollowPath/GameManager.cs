using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
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

        private int remainingBottles;


        public UnityEvent OnBottleHit;

        private void Awake()
        {
            OnBottleHit.AddListener(CheckToSpawnNewWave);
        }
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

        void CheckToSpawnNewWave()
        {
            remainingBottles--;
            Debug.Log("Bottle Hit " + remainingBottles);
            if (remainingBottles <= 0)
            {
                StartCoroutine(waveSpawner.SpawnWave(bottle.gameObject));
            }
        }

        public void GetNewBottlesCount(int count)
        {
            remainingBottles = count;
            Debug.Log("Spawned Bottles" + remainingBottles);

            //CheckToSpawnNewWave();
        }
    }
}
