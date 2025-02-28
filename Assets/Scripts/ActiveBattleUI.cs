using UnityEngine;

public class ActiveBattleUI : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.LEADERBOARD_CHANGED, OnLeaderBoardChanged);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_HEALTH_CHANGED, OnCharacterHealthChanged);
    }

    private void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.LEADERBOARD_CHANGED, OnLeaderBoardChanged);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_HEALTH_CHANGED, OnCharacterHealthChanged);
    }
    void OnLeaderBoardChanged(object obj)
    {
        //var data = ()obj
        Debug.Log($"Leaderboard changed {obj}");
    }

    void OnCharacterHealthChanged(object obj)
    {
        //var data = ()obj
        Debug.Log($"Player health changed {obj}");
    }
}
