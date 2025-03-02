using System.Linq;
using UnityEngine;

public class AmmoMovementSystem : BaseSystem
{
    void Update()
    {
        if (gameState == GameState.None || gameState == GameState.PreGame) return;

        var weapons = GameStateManager.Instance.GetAllWeapons();
        var ammoStates = GameStateManager.Instance.GetAllAmmo();
        var ammoIDs = GameStateManager.Instance.GetAllAmmoIDs();

        for (int i = 0; i < ammoIDs.Count(); i++)
        {
            var id = ammoIDs[i];
            AmmoState ammo = ammoStates[id];
            WeaponState weapon = weapons[ammo.weaponID];
            Vector3 movement = ammo.direction * ammo.speed * Time.deltaTime;
            Vector3 position = ammo.position + movement;
            bool exceededMaxRange = (position - ammo.initialPosition).magnitude > weapon.range;
            if (exceededMaxRange)
                position = ammo.initialPosition + ammo.direction.normalized * weapon.range;

            AmmoState newState = new AmmoState(ammo.id, position, ammo.direction, ammo.initialPosition, ammo.damage, ammo.speed, ammo.weaponID);
            messageQueue.Publish(GlobalSlugs.AMMO_STATE_CHANGED, newState);
            if (exceededMaxRange)
                messageQueue.Publish(GlobalSlugs.AMMO_DESPAWN, newState);
        }
    }
}
