using UnityEngine;

public class WonBattleUI : MonoBehaviour
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
        messageQueue.Subscribe(GlobalSlugs.PLAYER_WON, OnPlayerWon);
    }

    private void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_WON, OnPlayerWon);
    }

    void OnPlayerWon(object obj)
    {
        //var data = ()obj
        Debug.Log($"Player won {obj}");
    }
}
