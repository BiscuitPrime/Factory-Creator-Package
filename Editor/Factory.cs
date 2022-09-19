using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Modular Factory element
 *  Can be used in most projects to easily create factories in your projects
 *  Feel free to share, modify and use in your personal and profesional works
 *  
 *  @author : Henri 'Biscuit Prime' Nomico
 */
public class Factory : MonoBehaviour
{
    #region SINGLETON DESIGN PATTERN
    private static Factory _instance;
    public static Factory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Factory();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    #region OBJECT POOLING
    [System.Serializable]
    public class ObjectPool //class of the pool of spawnable objects
    {
        public string poolName;     //name of the pool
        public GameObject objectPrefab;  //prefab of the spawnable object
        public int poolSize;        //number of instantiated objects in the pool
    }

    public List<ObjectPool> poolsList; //list of all available pools
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(ObjectPool pool in poolsList)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();
            for(int i = 0; i < pool.poolSize; i++)
            {
                GameObject new_object = Instantiate(pool.objectPrefab);
                new_object.SetActive(false);
                new_object.transform.parent = this.transform;
                poolQueue.Enqueue(new_object);
            }
            poolDictionary.Add(pool.poolName, poolQueue);
        }
    }
    #endregion

    #region CREATE OBJECT FUNCTION
    /**
     *  The function called by other game objects to "spawn" an object. Returns the spawned object to allow for further modification of said object.
     *  @param name (string) : name of an associated pool of objects of the factory
     *  @return : GameObject : spawned object
     */
    public GameObject CreateObject(string name)
    {
        print("1");
        if (!poolDictionary.ContainsKey(name))
        {
            print("ERROR : no NAME");
            //throw new UnassignedReferenceException("Factory class : CreateObject() : inputted string<name> is not present in the pooldictionary");
        }
        print("2");
        GameObject spawnedObject = poolDictionary[name].Dequeue();
        print("3");
        if (spawnedObject == null)
        {
            throw new UnassignedReferenceException("Factory class : CreateObject() : spawnedObject is null");
        }
        spawnedObject.SetActive(true);
        poolDictionary[name].Enqueue(spawnedObject);
        print("4");
        return spawnedObject;
    }
    #endregion
}
