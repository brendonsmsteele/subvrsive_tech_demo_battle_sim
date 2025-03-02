using System.Drawing;
using UnityEngine;

public class PlayerFactory : MonoBehaviour, IFactory<PlayerRigController>
{
    public static PlayerFactory Instance { get; private set; }

    [SerializeField] MessageQueue messageQueue;
    [SerializeField] PrefabPooler playerPool;
    [SerializeField] SpawnPointsController spawnPoints;

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

    public PlayerRigController GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject go = playerPool.GetObject(position, rotation);
        return go.GetComponent<PlayerRigController>();
    }

    public void ReturnObject(GameObject go)
    {
        playerPool.ReturnObject(go);
    }
}
