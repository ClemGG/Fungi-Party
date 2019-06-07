﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {



    [SerializeField] private Dictionary<string, Queue<GameObject>> poolDictionary;
    public Pool[] pools;


    #region Singleton
    public static ObjectPooler instance;
    
    public void Awake()
    {
        instance = this;

        CreateObjects();
    }
    #endregion


   


    // Use this for initialization
    public void CreateObjects () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }


	}


    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag \'" + tag + "\' doesn't exist.");
            return null;
        }
        GameObject obj = poolDictionary[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.position = pos;
        obj.transform.rotation = rot;

        IPooledObject pooledObj = obj.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(obj);

        return obj;

    }



    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot, Transform parent)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag \'" + tag + "\' doesn't exist.");
            return null;
        }
        GameObject obj = poolDictionary[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.parent = parent;

        IPooledObject pooledObj = obj.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(obj);

        return obj;

    }






    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}
