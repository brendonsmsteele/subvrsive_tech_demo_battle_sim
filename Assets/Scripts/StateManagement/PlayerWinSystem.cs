using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWinSystem : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;

    void Start()
    {
        
    }

    void Update()
    {
        var players = GameStateManager.Instance.GetAllPlayers();
        int aliveCount = players.Values.Count(p => !p.isDead);

        if (aliveCount == 1)
        {
            Guid winnerID = players.Values.First(p => !p.isDead).id;
            Debug.Log($"Player {winnerID} has won the game!");
            messageQueue.Publish(GlobalSlugs.PLAYER_WON, winnerID);
        }
        else if(aliveCount == 0)
        {
            Debug.Log("DRAW!");
            messageQueue.Publish(GlobalSlugs.BATTLE_ENDED_IN_DRAW, "");
        }
    }
}
