using System;
using UnityEngine;

public class PlayerRotationSystem : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    void Update()
    {
        var players = GameStateManager.Instance.GetAllPlayers();

        foreach (var kvp in players)
        {
            PlayerState player = kvp.Value;
            if(player.targetID != Guid.Empty)
            {
                PlayerState targetPlayer = players[player.targetID];
                Vector3 directionFromPlayerToTarget = targetPlayer.position - player.position;
                Quaternion rotationFromPlayerToTarget = Quaternion.LookRotation(directionFromPlayerToTarget, Vector3.up);
                Quaternion smoothRotation = Quaternion.Slerp(player.rotation, rotationFromPlayerToTarget, player.rotateSpeed * Time.deltaTime);

                PlayerState newState = new PlayerState(player.id, player.position, smoothRotation, player.health, player.moveSpeed, player.rotateSpeed, player.attackDelay, true, player.targetID);
                messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
            }
        }
    }
}
