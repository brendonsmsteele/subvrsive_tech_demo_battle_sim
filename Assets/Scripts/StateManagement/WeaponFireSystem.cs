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
            if (player.isDead) continue;

            float baseAttackSpeed = 3f;
            float thisFrameAttachSpeed = baseAttackSpeed / weapon.attackSpeed;
            float randomDelay = Random.Range(0f, 1f);
            bool weaponOnCooldown = (Time.time - weapon.lastFired) < (thisFrameAttachSpeed + player.attackDelay + randomDelay);
            float lastFired = weapon.lastFired;
            if (!weaponOnCooldown)
            {
                // Fire
                Debug.Log($"Player {weapon.ownerID},  Weapon {id}");
                lastFired = Time.time;
                WeaponState newState = new WeaponState(weapon.id, lastFired, weapon.attackSpeed, weapon.range, weapon.ownerID);
                messageQueue.Publish(GlobalSlugs.WEAPON_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.WEAPON_FIRED, newState);
            }
        }
    }
}
