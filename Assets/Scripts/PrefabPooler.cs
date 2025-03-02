using UnityEngine;
using System.Collections.Generic;

public class PrefabPooler : MonoBehaviour
{
    public GameObject prefab;  // The object to pool
    public int initialPoolSize = 10;  // Starting pool size
    public int expandAmount = 5;  // How many new objects to add when expanding

    Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // Initialize the pool
        ExpandPool(initialPoolSize);
    }

    void ExpandPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            // Expand the pool when empty
            ExpandPool(expandAmount);
        }

        GameObject obj = pool.Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void ReturnAllObjects()
    {
        for(int i=0; i<pool.Count; i++)
        {
            ReturnObject(pool.Dequeue());
        }
    }
}
