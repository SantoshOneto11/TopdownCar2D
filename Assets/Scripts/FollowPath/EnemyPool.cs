using ShootBottle;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject bottlePrefab;
    public GameObject bulletPrefab;


    [SerializeField] public List<GameObject> pooledBottles;
    [SerializeField] private List<GameObject> pooledBullets;
    [SerializeField] private int poolSize = 10;

    private void Start()
    {
        pooledBottles = new List<GameObject>(poolSize);
        pooledBullets = new List<GameObject>(poolSize);

        FillPool(bottlePrefab, pooledBottles);
        FillPool(bulletPrefab, pooledBullets);
    }

    void FillPool(GameObject obj, List<GameObject> listName)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bottle = GeneratePooledObject(obj, listName);

            bottle.gameObject.SetActive(false);
        }
    }

    public GameObject GetPooledBottles(GameObject obj)
    {
        foreach (GameObject pooledObject in pooledBottles)
        {
            if (!pooledObject.activeInHierarchy)
            {
                pooledObject.SetActive(true);
                return pooledObject;
            }
        }
        return GeneratePooledObject(obj, pooledBottles);
    }

    public GameObject GetPooledBullets()
    {
        foreach (GameObject pooledObject in pooledBullets)
        {
            if (!pooledObject.activeInHierarchy)
            {
                pooledObject.SetActive(true);
                return pooledObject;
            }
        }
        return GeneratePooledObject(bulletPrefab, pooledBullets);
    }


    public void ReturnToPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }


    GameObject GeneratePooledObject(GameObject obj, List<GameObject> listName)
    {
        GameObject pooledObject = Instantiate(obj) as GameObject;

        listName.Add(pooledObject);
        pooledObject.transform.parent = transform;
        return pooledObject;
    }
}
