using TMPro;
using UnityEngine;

public class WonBattleUI : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] TMP_Text winner;

    string winningPlayer;

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
        winningPlayer = (string)obj;
        BindWinningPlayerText();
    }

    void BindWinningPlayerText()
    {
        winner.text = $"Player {winningPlayer} won!";
    }
}
