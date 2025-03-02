using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovementSystem : BaseSystem
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
            PlayerState targetPlayer = players[player.targetID];

            float stopShortDistance = 10f;
            float scaleMovementSoItFeelsGood = .3f;
            float thisFramePredictedMovement = player.moveSpeed * Time.deltaTime * scaleMovementSoItFeelsGood;
            Vector3 forwardRotation = targetPlayer.rotation * Vector3.forward;
            //Vector3 targetPlayersFacePosition = targetPlayer.position + forwardRotation.normalized * distanceFromTargetsFace;
            Vector3 vectorFromAToB = targetPlayer.position - player.position;
            Vector3 vectorFromAToBStopShort = vectorFromAToB.normalized * Mathf.Max(vectorFromAToB.magnitude - stopShortDistance, 0f);
            Vector3 clamp = vectorFromAToB.normalized * Mathf.Min(vectorFromAToBStopShort.magnitude, thisFramePredictedMovement);
            Vector3 thisFramePosition = player.position + clamp;

            Vector3 yPlaneLockedMovement = new Vector3(thisFramePosition.x, 0f, thisFramePosition.z); // HACK: Corrects for drift

            PlayerState newState = new PlayerState(player.id, yPlaneLockedMovement, player.rotation, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
            messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
            messageQueue.Publish(GlobalSlugs.PLAYER_POSITION_CHANGED, newState);
        }
    }
}