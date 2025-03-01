using System.Linq;

public class PlayerDeathSystem : BaseSystem
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

            if (player.health <= 0 && !player.isDead)
            {
                PlayerState newState = new PlayerState(player.id, player.position, player.rotation, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, true, player.targetID);
                messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.PLAYER_DIED, newState);
            }
        }
    }
}
