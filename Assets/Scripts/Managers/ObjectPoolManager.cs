using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public Dictionary<string, Pool> dictionnary;
    public List<Pool> pools;

    private Transform canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        dictionnary = new Dictionary<string, Pool>();

        foreach(Pool pool in pools)
        {
            for (int i = 0; i < pool.size; i++)
            {
                GameObject createdObject = Instantiate(pool.prefab, canvas);
                createdObject.SetActive(false);
                pool.list.Add(createdObject);
            }

            dictionnary.Add(pool.key, pool);
        }
    }

    public GameObject SpawnFromPool(string key, Vector3 position, Quaternion rotation)
    {
        if (!dictionnary.ContainsKey(key))
        {
            Debug.Log("There is no such pool with the key " + key);

            return null;
        }
        
        GameObject objectToSpawn = ReturnObjectFromList(key, position, rotation);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    private GameObject ReturnObjectFromList(string key, Vector3 position, Quaternion rotation)
    {
        Pool pool = dictionnary[key];

        foreach(GameObject pooledObject in pool.list)
        {
            if (!pooledObject.activeInHierarchy)
            {
                return pooledObject;
            }
        }

        GameObject newPoolObject = Instantiate(pool.prefab, position, rotation, canvas);
        pool.list.Add(newPoolObject);

        return newPoolObject;
    }
}
