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

        if(elgiblePlayersToBeTargeted.Count >= 2)
        {
            for (int i = 0; i < playerIDs.Count(); i++)
            {
                var id = playerIDs[i];
                PlayerState player = players[id];
                if (player.isDead) continue;

                int randIndex = UnityEngine.Random.Range(0, elgiblePlayersToBeTargeted.Count);
                var target = elgiblePlayersToBeTargeted[randIndex];

                // Avoid yourself choose the previous guy.
                if (target.Value.id == player.id)
                {
                    int bumpedIndex = randIndex == 0 ? elgiblePlayersToBeTargeted.Count - 1 : randIndex - 1;
                    target = elgiblePlayersToBeTargeted[bumpedIndex];
                }

                if (player.targetID == Guid.Empty || players[player.targetID].isDead)
                {
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
