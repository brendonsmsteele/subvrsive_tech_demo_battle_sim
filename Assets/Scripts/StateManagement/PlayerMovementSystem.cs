using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;

    void Update()
    {
        var players = GameStateManager.Instance.GetAllPlayers();

        foreach (var kvp in players)
        {
            PlayerState player = kvp.Value;
            PlayerState targetPlayer = players[player.targetID];

            Vector3 movement = new Vector3(1, 0, 1) * player.moveSpeed * Time.deltaTime;
            Vector3 toPosition = player.position + movement;

            PlayerState newState = new PlayerState(player.id, toPosition, player.rotation, player.health, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
            messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
        }
    }
}
