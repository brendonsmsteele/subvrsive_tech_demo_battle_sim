using TMPro;
using UnityEngine;

public class WonBattleUI : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] TMP_Text winner;

    string winningPlayer;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PLAYER_WON, HandlePlayerWon);
    }

    private void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_WON, HandlePlayerWon);
    }

    void HandlePlayerWon(object obj)
    {
        winningPlayer = (string)obj;
        BindWinningPlayerText();
    }

    void BindWinningPlayerText()
    {
        winner.text = $"Player {winningPlayer} won!";
    }
}
