using System.Drawing;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] PrefabPooler playerPool;
    [SerializeField] PrefabPooler ammoPool;
    [SerializeField] SpawnPointsController spawnPoints;

    void Start()
    {
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PRE_BATTLE_STARTED, HandlePreBattleStarted);
        messageQueue.Subscribe(GlobalSlugs.BATTLE_ENDED, HandleBattleEnded);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PRE_BATTLE_STARTED, HandlePreBattleStarted);
        messageQueue.Unsubscribe(GlobalSlugs.BATTLE_ENDED, HandleBattleEnded);
    }

    void HandlePreBattleStarted(object obj)
    {
        ClearPlayers();
        SpawnPlayers();
    }

    void HandleBattleEnded(object obj)
    {
    }

    void ClearPlayers()
    {
        playerPool.ReturnlAllObjects();
    }

    void SpawnPlayers()
    {
        foreach (Transform point in spawnPoints.points)
        {
            var go = SpawnPlayer(point);
        }
    }

    GameObject SpawnPlayer(Transform point)
    {
        var go = playerPool.GetObject(point.position, point.rotation);
        Debug.Log(go.name);
        go.transform.SetParent(transform, true);
        var player = go.GetComponent<PlayerRigController>();
        player.pool = ammoPool;
        return go;
    }
}
