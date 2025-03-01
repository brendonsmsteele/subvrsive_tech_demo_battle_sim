using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] MessageQueue messageQueue;
    Dictionary<Guid, PlayerState> players = new Dictionary<Guid, PlayerState>();
    Dictionary<Guid, WeaponState> weapons = new Dictionary<Guid, WeaponState>();
    Dictionary<Guid, AmmoState> ammo = new Dictionary<Guid, AmmoState>();

    IReadOnlyDictionary<Guid, PlayerState> _readOnlyPlayers;
    IReadOnlyDictionary<Guid, WeaponState> _readOnlyWeapons;
    IReadOnlyDictionary<Guid, AmmoState> _readOnlyAmmo;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, HandlePlayerAddedToBattle);
        messageQueue.Subscribe(GlobalSlugs.WEAPON_ADDED_TO_BATTLE, HandleWeaponAddedToBattle);
        messageQueue.Subscribe(GlobalSlugs.AMMO_ADDED_TO_BATTLE, HandleAmmoAddedToBattle);

        messageQueue.Subscribe(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, HandlePlayerRemovedFromBattle);
        messageQueue.Subscribe(GlobalSlugs.WEAPON_REMOVED_FROM_BATTLE, HandleWeaponRemovedFromBattle);
        messageQueue.Subscribe(GlobalSlugs.AMMO_REMOVED_FROM_BATTLE, HandleAmmoRemovedFromBattle);

        messageQueue.Subscribe(GlobalSlugs.PLAYER_STATE_CHANGED, HandlePlayerStateChanged);
        messageQueue.Subscribe(GlobalSlugs.WEAPON_STATE_CHANGED, HandleWeaponStateChanged);
        messageQueue.Subscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, HandlePlayerAddedToBattle);
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_ADDED_TO_BATTLE, HandleWeaponAddedToBattle);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_ADDED_TO_BATTLE, HandleAmmoAddedToBattle);

        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, HandlePlayerRemovedFromBattle);
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_REMOVED_FROM_BATTLE, HandleWeaponRemovedFromBattle);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_REMOVED_FROM_BATTLE, HandleAmmoRemovedFromBattle);

        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_STATE_CHANGED, HandlePlayerStateChanged);
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_STATE_CHANGED, HandleWeaponStateChanged);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);
    }

    public IReadOnlyDictionary<Guid, PlayerState> GetAllPlayers()
    {
        return _readOnlyPlayers ??= players;
    }

    public IReadOnlyDictionary<Guid, WeaponState> GetAllWeapons()
    {
        return _readOnlyWeapons ??= weapons;
    }

    public IReadOnlyDictionary<Guid, AmmoState> GetAllAmmo()
    {
        return _readOnlyAmmo ??= ammo;
    }

    #region Handlers

    //  Add
    void HandlePlayerAddedToBattle(object obj)
    {
        var player = (PlayerState)obj;
        if(!players.ContainsKey(player.id))
            players[player.id] = player;
    }

    void HandleWeaponAddedToBattle(object obj)
    {
        var weapon = (WeaponState)obj;
        if (!weapons.ContainsKey(weapon.id))
            weapons[weapon.id] = weapon;
    }

    void HandleAmmoAddedToBattle(object obj)
    {
        var ammoState = (AmmoState)obj;
        if (!ammo.ContainsKey(ammoState.id))
            ammo[ammoState.id] = ammoState;
    }


    //  Remove
    void HandlePlayerRemovedFromBattle(object obj)
    {
        var id = (Guid)obj;
        if (players.ContainsKey(id))
            players.Remove(id);
    }

    void HandleWeaponRemovedFromBattle(object obj)
    {
        var id = (Guid)obj;
        if (weapons.ContainsKey(id))
            weapons.Remove(id);
    }

    void HandleAmmoRemovedFromBattle(object obj)
    {
        var id = (Guid)obj;
        if (ammo.ContainsKey(id))
            ammo.Remove(id);
    }

    //  State Changed
    void HandlePlayerStateChanged(object obj)
    {
        var player = (PlayerState)obj;
        players[player.id] = player;
    }

    void HandleWeaponStateChanged(object obj)
    {
        var weapon = (WeaponState)obj;
        weapons[weapon.id] = weapon;
    }

    void HandleAmmoStateChanged(object obj)
    {
        var ammoState = (AmmoState)obj;
        ammo[ammoState.id] = ammoState;
    }

    #endregion
}
