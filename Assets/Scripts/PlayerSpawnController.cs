using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] PrefabPooler pool;
    [SerializeField] SpawnPointsController spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
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
        pool.ReturnlAllObjects();
    }

    void SpawnPlayers()
    {
        foreach (Transform point in spawnPoints.points)
        {
            var go = pool.GetObject(point.position, point.rotation);
            go.transform.SetParent(transform, true);
        }
    }
}
