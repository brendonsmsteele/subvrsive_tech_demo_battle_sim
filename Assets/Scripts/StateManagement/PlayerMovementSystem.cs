using System.Linq;
using UnityEngine;

public class PlayerMovementSystem : BaseSystem
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
            PlayerState targetPlayer = players[player.targetID];

            Vector3 movement = new Vector3(1, 0, 1) * player.moveSpeed * Time.deltaTime;
            Vector3 toPosition = player.position + movement;

            PlayerState newState = new PlayerState(player.id, toPosition, player.rotation, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
            messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
            messageQueue.Publish(GlobalSlugs.PLAYER_POSITION_CHANGED, newState);
        }
    }
}
