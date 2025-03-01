using UnityEngine;

public class PlayerHitSystem : BaseSystem
{
    protected override void OnEnable()
    {
        base.OnEnable();
        messageQueue.Subscribe(GlobalSlugs.PLAYER_HIT, HandlePlayerHit);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_HIT, HandlePlayerHit);
    }

    void HandlePlayerHit(object obj)
    {
        if (gameState == GameState.None || gameState == GameState.PreGame) return;

        var playerHit = (PlayerHitState)obj;
        var players = GameStateManager.Instance.GetAllPlayers();
        var ammoStates = GameStateManager.Instance.GetAllAmmo();
        var player = players[playerHit.playerID];
        var ammo = ammoStates[playerHit.ammoID];
        var playerHealthAfterDamage = player.health - ammo.damage;
        var changedPlayerState = new PlayerState(player.id, player.position, player.rotation, playerHealthAfterDamage, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
        Debug.Log($"Player {player.id} - hit and damaged by ammo {ammo.id} - player's playerHealthAfterDamage is {playerHealthAfterDamage}");
        messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, changedPlayerState);
        messageQueue.Publish(GlobalSlugs.PLAYER_DAMAGED, changedPlayerState);
    }
}
