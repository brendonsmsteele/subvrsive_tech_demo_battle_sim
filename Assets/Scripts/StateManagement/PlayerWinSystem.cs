using System;
using System.Linq;
using UnityEngine;

public class PlayerWinSystem : BaseSystem
{
    void Update()
    {
        if (gameState != GameState.ActiveGame) return;

        var players = GameStateManager.Instance.GetAllPlayers();
        int aliveCount = players.Values.Count(p => !p.isDead);

        if (aliveCount == 1)
        {
            Guid winnerID = players.Values.First(p => !p.isDead).id;
            Debug.Log($"Player {winnerID} has won the game!");
            messageQueue.Publish(GlobalSlugs.PRE_BATTLE_ENDED, "");
            messageQueue.Publish(GlobalSlugs.PLAYER_WON, winnerID);
            messageQueue.Publish(GlobalSlugs.GAME_ENDED, "");
        }
        else if(aliveCount == 0)
        {
            Debug.Log("DRAW!");
            messageQueue.Publish(GlobalSlugs.PRE_BATTLE_ENDED, "");
            messageQueue.Publish(GlobalSlugs.BATTLE_ENDED_IN_DRAW, "");
            messageQueue.Publish(GlobalSlugs.GAME_ENDED, "");
        }
    }
}
