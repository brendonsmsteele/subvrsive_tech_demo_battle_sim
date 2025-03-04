using System;
using System.Linq;
using UnityEngine;

public class PlayerRotationSystem : BaseSystem
{
    void Update()
    {
        if (gameState == GameState.None || gameState == GameState.PreGame) return;

        var players = GameStateManager.Instance.GetAllPlayers();
        var playerIDs = GameStateManager.Instance.GetAllPlayerIDs();

        for (int i = 0; i < playerIDs.Count(); i++)
        {
            var id = playerIDs[i];
            PlayerState player = players[id];
            if (player.isDead) continue;

            if (player.targetID != Guid.Empty)
            {
                PlayerState targetPlayer = players[player.targetID];
                Vector3 directionFromPlayerToTarget = targetPlayer.position - player.position;
                Quaternion rotationFromPlayerToTarget = Quaternion.LookRotation(directionFromPlayerToTarget, Vector3.up);

                PlayerState newState = new PlayerState(player.id, player.position, rotationFromPlayerToTarget, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
                messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.PLAYER_ROTATION_CHANGED, newState);
            }
        }
    }
}
