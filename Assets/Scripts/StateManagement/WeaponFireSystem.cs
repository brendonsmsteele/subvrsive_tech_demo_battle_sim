using UnityEngine;

public class WeaponFireSystem : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    void Update()
    {
        var players = GameStateManager.Instance.GetAllPlayers();
        var weapons = GameStateManager.Instance.GetAllWeapons();

        foreach (var kvp in weapons)
        {
            WeaponState weapon = kvp.Value;
            PlayerState player = players[weapon.ownerID];

            bool weaponOnCooldown = (Time.time - weapon.lastFired) < (weapon.attackSpeed + player.attackDelay);
            float lastFired = weapon.lastFired;
            if (!weaponOnCooldown)
            {
                // Fire
                lastFired = Time.time;
                WeaponState newState = new WeaponState(weapon.id, lastFired, weapon.attackSpeed, weapon.range, weapon.ownerID);
                messageQueue.Publish(GlobalSlugs.WEAPON_STATE_CHANGED, newState);
                messageQueue.Publish(GlobalSlugs.WEAPON_FIRED, newState);
            }
        }
    }
}
