using System;
using System.Linq;
using UnityEngine;

public class PlayerRotationSystem : BaseSystem
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
            if(player.targetID != Guid.Empty)
            {
                PlayerState targetPlayer = players[player.targetID];
                Vector3 directionFromPlayerToTarget = targetPlayer.position - player.position;
                Quaternion rotationFromPlayerToTarget = Quaternion.LookRotation(directionFromPlayerToTarget, Vector3.up);

                //Quaternion smoothRotation = Quaternion.Slerp(player.rotation, rotationFromPlayerToTarget, player.rotateSpeed * Time.deltaTime);
                //Quaternion yAxisAlignedRotation = WorldYAxisAlignedRotation(smoothRotation);

                PlayerState newState = new PlayerState(player.id, player.position, rotationFromPlayerToTarget, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
                messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.PLAYER_ROTATION_CHANGED, newState);
            }
        }
    }

    Quaternion WorldYAxisAlignedRotation(Quaternion rotation)
    {
        // Extract the forward direction (local Z-axis)
        Vector3 forward = rotation * Vector3.forward;

        // Project forward onto the X-Z plane (removing vertical tilt)
        forward.y = 0;
        forward.Normalize();

        // Construct a new quaternion using world up (Y-axis) and corrected forward
        return Quaternion.LookRotation(forward, Vector3.up);
    }
}
