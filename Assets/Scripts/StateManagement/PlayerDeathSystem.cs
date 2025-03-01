using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathSystem : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;

    void Update()
    {
        var players = GameStateManager.Instance.GetAllPlayers();

        foreach (var kvp in players)
        {
            PlayerState player = kvp.Value;

            if (player.health <= 0 && !player.isDead)
            {
                PlayerState newState = new PlayerState(player.id, player.position, player.rotation, player.health, player.moveSpeed, player.rotateSpeed, player.attackDelay, true, player.targetID);
                messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.PLAYER_DIED, newState);
            }
        }
    }
}
