using System.Linq;
using UnityEngine;

public class WeaponFireSystem : BaseSystem
{
    void Update()
    {
        if (gameState != GameState.ActiveGame) return;

        var players = GameStateManager.Instance.GetAllPlayers();
        var weapons = GameStateManager.Instance.GetAllWeapons();
        var weaponIDs = GameStateManager.Instance.GetAllWeaponIDs();

        for(int i = 0; i< weaponIDs.Count(); i++)
        {
            var id = weaponIDs[i];
            WeaponState weapon = weapons[id];
            PlayerState player = players[weapon.ownerID];

            bool weaponOnCooldown = (Time.time - weapon.lastFired) < (weapon.attackSpeed + player.attackDelay);
            float lastFired = weapon.lastFired;
            if (!weaponOnCooldown)
            {
                // Fire
                Debug.Log($"Player {weapon.ownerID}, fired weapon {weapon.id}");
                lastFired = Time.time;
                WeaponState newState = new WeaponState(weapon.id, lastFired, weapon.attackSpeed, weapon.range, weapon.ammoAnchorPosition, weapon.ammoAnchorRotation, weapon.ownerID);
                messageQueue.Publish(GlobalSlugs.WEAPON_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.WEAPON_FIRED, newState);
            }
        }
    }
}
