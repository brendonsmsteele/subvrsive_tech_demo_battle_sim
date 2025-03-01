using UnityEngine;
using static GameLoopManager;

public class BaseSystem : MonoBehaviour
{
    [SerializeField] protected MessageQueue messageQueue;
    protected GameState gameState;

    protected virtual void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.GAME_STATE_CHANGED, HandleGameStateChanged);
    }

    protected virtual void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.GAME_STATE_CHANGED, HandleGameStateChanged);
    }

    void HandleGameStateChanged(object obj)
    {
        var gameState = (GameState)obj;
    }
}
