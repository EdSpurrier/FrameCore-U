using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCore : MonoBehaviour
{

    [System.Serializable]
    public class PoolObject : System.Object
    {
        [HideLabel]
        [HorizontalGroup("Split", 0.50f)]
        public Transform prefab;
        [HideLabel]
        [HorizontalGroup("Split", 0.50f)]
        [SuffixLabel("Amount", Overlay = true)]
        public int amount = 50;
        [FoldoutGroup("Pool Object")]
        public List<GameObject> objects;
        [FoldoutGroup("Pool Object")]
        public float lazyLoadTime = 0f;
        [FoldoutGroup("Pool Object")]
        public bool boosting = false;
    }

    [Title("Object Pool")]
    public List<PoolObject> poolObjects;

    [Title("Settings")]
    [HideLabel]
    [HorizontalGroup("Split", 0.50f)]
    [SuffixLabel("Minimum Amount", Overlay = true)]
    public int minimumPoolAmount = 5;
    [Title("")]
    [HideLabel]
    [HorizontalGroup("Split", 0.50f)]
    [SuffixLabel("Lazy Load Time", Overlay = true)]
    public float lazyLoadTime = 2f;

    [Title("System")]
    public float poolTime = 0f;

    public DeBugger debug;


    public void AddToPool(Transform prefab)
    {
        bool found = false;
        foreach (PoolObject poolObject in poolObjects)
        {
            if (prefab == poolObject.prefab)
            {
                found = true;
            };
        };

        if (!found)
        {
            PoolObject poolObject = new PoolObject();
            poolObject.prefab = prefab;
            poolObjects.Add(poolObject);
        };
    }

    public bool ObjectInPool(Transform prefab)
    {
        bool found = false;
        foreach (PoolObject poolObject in poolObjects)
        {
            if (prefab == poolObject.prefab)
            {
                found = true;
            };
        };

        return found;
    }

    void Update()
    {
        poolTime += Time.deltaTime;
        BoostPoolCycle();
    }


    public void BoostPoolCycle()
    {
        foreach (PoolObject poolObject in poolObjects)
        {
            if (poolObject.boosting)
            {
                if (poolObject.lazyLoadTime <= 0)
                {
                    CreateSpawnableObject(poolObject.prefab, poolObject);

                    if (poolObject.objects.Count >= poolObject.amount)
                    {
                        poolObject.boosting = false;
                    }
                    else
                    {
                        poolObject.lazyLoadTime = lazyLoadTime;
                    };
                }
                else
                {
                    poolObject.lazyLoadTime -= Time.deltaTime;
                };

            };
        }
    }




    private void Awake()
    {
        CreateObjects();
    }


    void CreateObjects()
    {

        foreach (PoolObject poolObject in poolObjects)
        {
            for (int i = 0; i < poolObject.amount; i++)
            {
                CreateSpawnableObject(poolObject.prefab, poolObject);
            };

            poolObject.boosting = false;
        };

    }


    public void CreateSpawnableObject(Transform obj, PoolObject poolObject)
    {
        Transform newObj = Instantiate(obj, Vector3.zero, Quaternion.identity) as Transform;
        newObj.transform.SetParent(transform);
        newObj.gameObject.SetActive(false);

        poolObject.objects.Add(newObj.gameObject);
    }







    public GameObject SpawnObject(Transform obj, Vector3 position, Quaternion rotation)
    {
        bool found = false;

        GameObject spawnedObject = null;

        foreach (PoolObject poolObject in poolObjects)
        {

            if (poolObject.prefab == obj)
            {
                //  INSTANT SPAWN
                if (poolObject.objects.Count < minimumPoolAmount)
                {
                    CreateSpawnableObject(poolObject.prefab, poolObject);
                    poolObject.boosting = true;
                };

                spawnedObject = poolObject.objects[0];

                poolObject.objects.Remove(spawnedObject);

                spawnedObject.transform.SetParent(null);

                spawnedObject.transform.position = position;
                spawnedObject.transform.rotation = rotation;

                spawnedObject.SetActive(true);

                found = true;
            };

        };

        if (!found)
        {
            Debug.LogError("No Object Found In Pool... " + obj.name);
            Debug.Break();
            return null;
        };

        return spawnedObject;
    }



}
