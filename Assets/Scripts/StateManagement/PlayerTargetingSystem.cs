using System;
using System.Linq;
using UnityEngine;
using static GameLoopManager;

public class PlayerTargetingSystem : BaseSystem
{
    void Update()
    {
        if (gameState != GameState.ActiveGame) return;

        var players = GameStateManager.Instance.GetAllPlayers();
        var elgiblePlayersToBeTargeted = players.Where(p => !p.Value.isDead).ToList();

        foreach (var kvp in players)
        {
            PlayerState player = kvp.Value;
            int randIndex = UnityEngine.Random.Range(0, elgiblePlayersToBeTargeted.Count);

            if (player.targetID == Guid.Empty || players[player.targetID].isDead)
            {
                var target = elgiblePlayersToBeTargeted[randIndex];
                var targetID = target.Key;
                PlayerState newState = new PlayerState(player.id, player.position, player.rotation, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, targetID);
                messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.PLAYER_ACQUIRED_TARGET, newState);
                messageQueue.Publish(GlobalSlugs.PLAYER_TARGETED, target.Value);
            }
        }
    }
}
