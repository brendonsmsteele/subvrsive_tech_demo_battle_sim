using System;
using System.Linq;

public class PlayerTargetingSystem : BaseSystem
{
    void Update()
    {
        if (gameState != GameState.ActiveGame) return;

        var players = GameStateManager.Instance.GetAllPlayers();
        var playerIDs = GameStateManager.Instance.GetAllPlayerIDs();
        var elgiblePlayersToBeTargeted = players.Where(p => !p.Value.isDead).ToList();

        if(elgiblePlayersToBeTargeted.Count > 0)
        {
            for (int i = 0; i < playerIDs.Count(); i++)
            {
                var id = playerIDs[i];
                PlayerState player = players[id];
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
}
