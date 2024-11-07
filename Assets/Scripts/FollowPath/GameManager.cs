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

        [SerializeField] private int objectsInWave = 5;
        [SerializeField] private int varience = 3;
        [SerializeField] private int totalWave = 5;
        [SerializeField] private float spawnInterval = 2f;

        private void Start()
        {
            StartCoroutine(SpawnWave(bottle.gameObject));
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                gun.Shoot(poolManager.GetPooledBullets());
            }
        }


        private IEnumerator SpawnWave(GameObject obj)
        {
            int currentWaveCount = Random.Range(objectsInWave, objectsInWave + varience);
            for (int i = 0; i < currentWaveCount; i++)
            {
                GameObject bottle = poolManager.GetPooledBottles(obj);
                if (bottle != null)
                {
                    bottle.SetActive(true);
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        public void SendPoolData(GameObject obj)
        {
            poolManager.ReturnToPool(obj);
        }
    }
}
