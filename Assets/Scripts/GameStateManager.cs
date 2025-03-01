using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] MessageQueue messageQueue;
    Dictionary<Guid, PlayerState> players = new Dictionary<Guid, PlayerState>();
    Dictionary<Guid, PlayerBodyState> playerBodies = new Dictionary<Guid, PlayerBodyState>();
    Dictionary<Guid, WeaponState> weapons = new Dictionary<Guid, WeaponState>();
    Dictionary<Guid, AmmoState> ammo = new Dictionary<Guid, AmmoState>();

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
        messageQueue.Subscribe(GlobalSlugs.PLAYER_BODY_ADDED_TO_BATTLE, HandlePlayerBodyAddedToBattle);
        messageQueue.Subscribe(GlobalSlugs.WEAPON_ADDED_TO_BATTLE, HandleWeaponAddedToBattle);
        messageQueue.Subscribe(GlobalSlugs.AMMO_ADDED_TO_BATTLE, HandleAmmoAddedToBattle);

        messageQueue.Subscribe(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, HandlePlayerRemovedFromBattle);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_BODY_REMOVED_FROM_BATTLE, HandlePlayerBodyRemovedFromBattle);
        messageQueue.Subscribe(GlobalSlugs.WEAPON_REMOVED_FROM_BATTLE, HandleWeaponRemovedFromBattle);
        messageQueue.Subscribe(GlobalSlugs.AMMO_REMOVED_FROM_BATTLE, HandleAmmoRemovedFromBattle);

        messageQueue.Subscribe(GlobalSlugs.PLAYER_STATE_CHANGED, HandlePlayerStateChanged);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_BODY_STATE_CHANGED, HandlePlayerBodyStateChanged);
        messageQueue.Subscribe(GlobalSlugs.WEAPON_STATE_CHANGED, HandleWeaponStateChanged);
        messageQueue.Subscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, HandlePlayerAddedToBattle);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_BODY_ADDED_TO_BATTLE, HandlePlayerBodyAddedToBattle);
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_ADDED_TO_BATTLE, HandleWeaponAddedToBattle);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_ADDED_TO_BATTLE, HandleAmmoAddedToBattle);

        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, HandlePlayerRemovedFromBattle);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_BODY_REMOVED_FROM_BATTLE, HandlePlayerBodyRemovedFromBattle);
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_REMOVED_FROM_BATTLE, HandleWeaponRemovedFromBattle);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_REMOVED_FROM_BATTLE, HandleAmmoRemovedFromBattle);

        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_STATE_CHANGED, HandlePlayerStateChanged);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_BODY_STATE_CHANGED, HandlePlayerBodyStateChanged);
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_STATE_CHANGED, HandleWeaponStateChanged);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);
    }

    //bool CheckIfPlayerHealthCausedDeath()
    //{
    //    var livingPlayers = players.Values.Select(p => !p.isDead);
    //    foreach (PlayerState player in livingPlayers)
    //    {
    //        if (player.health <= 0f)
    //        {
    //            players[player.id] = new PlayerState(player.id, player.position, player.rotation, player.health, player.moveSpeed, player.rotateSpeed, player.isDead);
    //        }
    //    }
    //    return 
    //}

    //bool CheckIfPlayerWon()
    //{
    //    bool onePlayerLeft = players.Values.Select(p => !p.isDead).Count() <= 1;
    //    return onePlayerLeft;
    //}

    #region Handlers

    //  Add
    void HandlePlayerAddedToBattle(object obj)
    {
        var player = (PlayerState)obj;
        if(!players.ContainsKey(player.id))
            players[player.id] = player;
    }

    void HandlePlayerBodyAddedToBattle(object obj)
    {
        var body = (PlayerBodyState)obj;
        if (!playerBodies.ContainsKey(body.id))
            playerBodies[body.id] = body;
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
        var player = (PlayerState)obj;
        if (players.ContainsKey(player.id))
            players.Remove(player.id);
    }

    void HandlePlayerBodyRemovedFromBattle(object obj)
    {
        var body = (PlayerBodyState)obj;
        if (playerBodies.ContainsKey(body.id))
            playerBodies.Remove(body.id);
    }

    void HandleWeaponRemovedFromBattle(object obj)
    {
        var weapon = (WeaponState)obj;
        if (weapons.ContainsKey(weapon.id))
            weapons.Remove(weapon.id);
    }

    void HandleAmmoRemovedFromBattle(object obj)
    {
        var ammoState = (AmmoState)obj;
        if (ammo.ContainsKey(ammoState.id))
            ammo.Remove(ammoState.id);
    }

    //  State Changed
    void HandlePlayerStateChanged(object obj)
    {
        var player = (PlayerState)obj;
        players[player.id] = player;
    }

    void HandlePlayerBodyStateChanged(object obj)
    {
        var body = (PlayerBodyState)obj;
        playerBodies[body.id] = body;

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
