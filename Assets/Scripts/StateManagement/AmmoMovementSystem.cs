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

            AmmoState newState = new AmmoState(ammo.id, ammo.position + movement, ammo.direction, ammo.damage, ammo.speed);
            messageQueue.Publish(GlobalSlugs.AMMO_STATE_CHANGED, newState);
        }
    }
}
