using System.Linq;
using UnityEngine;

public class AmmoMovementSystem : BaseSystem
{
    void Update()
    {
        if (gameState == GameState.None || gameState == GameState.PreGame) return;

        var ammoStates = GameStateManager.Instance.GetAllAmmo();
        var ammoIDs = GameStateManager.Instance.GetAllAmmoIDs();

        for (int i = 0; i < ammoIDs.Count(); i++)
        {
            var id = ammoIDs[i];
            AmmoState ammo = ammoStates[id];
            Vector3 movement = ammo.direction * ammo.speed * Time.deltaTime;
            Vector3 position = ammo.position + movement;
            bool exceededMaxRange = (position - ammo.initialPosition).magnitude > ammo.range;
            if (exceededMaxRange)
                position = ammo.initialPosition + ammo.direction.normalized * ammo.range;

            AmmoState newState = new AmmoState(ammo.id, position, ammo.direction, ammo.initialPosition, ammo.damage, ammo.speed, ammo.range);
            messageQueue.Publish(GlobalSlugs.AMMO_STATE_CHANGED, newState);
            if (exceededMaxRange)
                messageQueue.Publish(GlobalSlugs.AMMO_DESPAWN, newState);
        }
    }
}
