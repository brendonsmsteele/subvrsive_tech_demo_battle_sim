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

            float stopShortInMeters = 5f;
            float scaleMovementSoItFeelsGood = .3f;
            float thisFrameIntendedMagnitude = player.moveSpeed * Time.deltaTime * scaleMovementSoItFeelsGood;
            Vector3 targetPlayersFacePosition = targetPlayer.position + targetPlayer.rotation.eulerAngles.normalized;
            Vector3 vectorFromAToB = targetPlayersFacePosition - player.position;
            float aToBeMagnitude = vectorFromAToB.magnitude;
            Vector3 clamp = vectorFromAToB.normalized * Mathf.Max(0f, aToBeMagnitude - stopShortInMeters);
            Vector3 thisFramePosition = player.position + clamp;
            Vector3 yPlaneLockedMovement = new Vector3(thisFramePosition.x, 0f, thisFramePosition.z);

            PlayerState newState = new PlayerState(player.id, yPlaneLockedMovement, player.rotation, player.health, player.maxHealth, player.moveSpeed, player.rotateSpeed, player.attackDelay, player.isDead, player.targetID);
            messageQueue.Publish(GlobalSlugs.PLAYER_STATE_CHANGED, newState);
            messageQueue.Publish(GlobalSlugs.PLAYER_POSITION_CHANGED, newState);
        }
    }
}
