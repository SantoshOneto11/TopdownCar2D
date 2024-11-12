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
        [SerializeField] private Magzine magzine;

        [SerializeField] private float spawnInterval = 2f;

        private int remainingBottles;


        public UnityEvent OnBottleHitEvent;
        public UnityEvent<int> OnShotFiredEvent;

        private void Awake()
        {
            OnBottleHitEvent.AddListener(CheckToSpawnNewWave);
            OnShotFiredEvent.AddListener(magzine.ShotFired);
        }
        private void Start()
        {
            StartCoroutine(waveSpawner.SpawnWave(bottle.gameObject));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && magzine.ActiveShells >= 0)
            {
                Debug.Log("Shot Fired!");
                OnShotFiredEvent.Invoke(1);
                gun.Shoot(poolManager.GetPooledBullets());
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("RightClick");
                //magzine.ReloadShells();
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
                StartCoroutine(WaitBeforeWave());
            }
        }

        IEnumerator WaitBeforeWave()
        {
            yield return new WaitForSeconds(spawnInterval);
            StartCoroutine(waveSpawner.SpawnWave(bottle.gameObject));
        }

        public void GetNewBottlesCount(int count)
        {
            remainingBottles = count;
            Debug.Log("Spawned Bottles" + remainingBottles);

            //CheckToSpawnNewWave();
        }
    }
}
