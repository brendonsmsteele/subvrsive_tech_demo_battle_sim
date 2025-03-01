using System;
using System.Collections.Generic;
using UnityEngine;
using static GameLoopManager;

public class PlayerDeathSystem : BaseSystem
{
    void Update()
    {
        if (gameState == GameState.None || gameState == GameState.PreGame) return;

        var players = GameStateManager.Instance.GetAllPlayers();

        foreach (var kvp in players)
        {
            PlayerState player = kvp.Value;

            if (player.health <= 0 && !player.isDead)
            {
                PlayerState newState = new PlayerState(player.id, player.position, player.rotation, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, true, player.targetID);
                messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.PLAYER_DIED, newState);
            }
        }
    }
}
