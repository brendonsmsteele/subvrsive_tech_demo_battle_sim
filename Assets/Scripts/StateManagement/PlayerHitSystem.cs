using UnityEngine;

public class PlayerHitSystem : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PLAYER_HIT, HandlePlayerHit);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_HIT, HandlePlayerHit);
    }

    void HandlePlayerHit(object obj)
    {
        var playerHit = (PlayerHitState)obj;
        var players = GameStateManager.Instance.GetAllPlayers();
        var ammoStates = GameStateManager.Instance.GetAllAmmo();
        var player = players[playerHit.playerID];
        var ammo = ammoStates[playerHit.ammoID];
        var playerHealthAfterDamage = player.health - ammo.damage;
        var changedPlayerState = new PlayerState(player.id, player.position, player.rotation, playerHealthAfterDamage, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
        messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, changedPlayerState);
        messageQueue.Publish(GlobalSlugs.PLAYER_DAMAGED, changedPlayerState);
    }
}
