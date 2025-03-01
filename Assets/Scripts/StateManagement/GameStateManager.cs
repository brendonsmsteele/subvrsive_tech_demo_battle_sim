using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] MessageQueue messageQueue;
    Dictionary<Guid, PlayerState> _players = new Dictionary<Guid, PlayerState>();
    Dictionary<Guid, WeaponState> _weapons = new Dictionary<Guid, WeaponState>();
    Dictionary<Guid, AmmoState> _ammo = new Dictionary<Guid, AmmoState>();


    Guid[] _playersCachedIDs;
    Guid[] _weaponsCachedIDs;
    Guid[] _ammoCachedIDs;

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

    // Get dictionaries
    public IReadOnlyDictionary<Guid, PlayerState> GetAllPlayers()
    {
        return _readOnlyPlayers ??= _players;
    }

    public IReadOnlyDictionary<Guid, WeaponState> GetAllWeapons()
    {
        return _readOnlyWeapons ??= _weapons;
    }

    public IReadOnlyDictionary<Guid, AmmoState> GetAllAmmo()
    {
        return _readOnlyAmmo ??= _ammo;
    }

    // Get ids
    public Guid[] GetAllPlayerIDs()
    {
        return _playersCachedIDs;
    }

    public Guid[] GetAllWeaponIDs()
    {
        return _weaponsCachedIDs;
    }

    public Guid[] GetAllAmmoIDs()
    {
        return _ammoCachedIDs;
    }

    #region Handlers

    //  Add
    void HandlePlayerAddedToBattle(object obj)
    {
        var player = (PlayerState)obj;
        if(!_players.ContainsKey(player.id))
            _players[player.id] = player;
            _playersCachedIDs = _players.Keys.ToArray();
    }

    void HandleWeaponAddedToBattle(object obj)
    {
        var weapon = (WeaponState)obj;
        if (!_weapons.ContainsKey(weapon.id))
            _weapons[weapon.id] = weapon;
            _weaponsCachedIDs = _weapons.Keys.ToArray();
    }

    void HandleAmmoAddedToBattle(object obj)
    {
        var ammoState = (AmmoState)obj;
        if (!_ammo.ContainsKey(ammoState.id))
            _ammo[ammoState.id] = ammoState;
            _ammoCachedIDs = _ammo.Keys.ToArray();
    }


    //  Remove
    void HandlePlayerRemovedFromBattle(object obj)
    {
        var id = (Guid)obj;
        if (_players.ContainsKey(id))
            _players.Remove(id);
            _playersCachedIDs = _players.Keys.ToArray();
    }

    void HandleWeaponRemovedFromBattle(object obj)
    {
        var id = (Guid)obj;
        if (_weapons.ContainsKey(id))
            _weapons.Remove(id);
            _weaponsCachedIDs = _weapons.Keys.ToArray();
    }

    void HandleAmmoRemovedFromBattle(object obj)
    {
        var id = (Guid)obj;
        if (_ammo.ContainsKey(id))
            _ammo.Remove(id);
            _ammoCachedIDs = _ammo.Keys.ToArray();
    }

    //  State Changed
    void HandlePlayerStateChanged(object obj)
    {
        var player = (PlayerState)obj;
        _players[player.id] = player;
    }

    void HandleWeaponStateChanged(object obj)
    {
        var weapon = (WeaponState)obj;
        _weapons[weapon.id] = weapon;
    }

    void HandleAmmoStateChanged(object obj)
    {
        var ammoState = (AmmoState)obj;
        _ammo[ammoState.id] = ammoState;
    }

    #endregion
}
