using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] SpawnPointsController spawnPointsController;

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PRE_BATTLE_STARTED, HandlePreBattleStarted);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PRE_BATTLE_STARTED, HandlePreBattleStarted);
    }

    void HandlePreBattleStarted(object obj)
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        var points = spawnPointsController.points;
        foreach (Transform point in points)
        {
            PlayerRigController player = PlayerFactory.Instance.GetObject(point.position, point.rotation);
            player.transform.SetParent(transform, true);
        }
    }
}
