using System;
using UnityEngine;

public class AmmoFactory : MonoBehaviour, IFactory<AmmoController>
{
    public static AmmoFactory Instance { get; private set; }

    [SerializeField] MessageQueue messageQueue;
    [SerializeField] PrefabPooler ammoPooler;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    public AmmoController GetObject(Vector3 position, Quaternion rotation, bool setActive = true)
    {
        GameObject go = ammoPooler.GetObject(position, rotation, setActive);
        return go.GetComponent<AmmoController>();
    }

    public void ReturnObject(GameObject go)
    {
        ammoPooler.ReturnObject(go);
    }
}