using System;
using UnityEngine;
public struct PlayerState
{
    public Guid id;
    public Vector3 position;
    public Quaternion rotation;
    public float health;
    public float maxHealth;
    public float moveSpeed;
    public float rotateSpeed;
    public float attackDelay;
    public bool isDead;
    public Guid targetID;

    public PlayerState(Guid id, Vector3 position, Quaternion rotation, float health, float maxHealth, float moveSpeed, float rotateSpeed, float attackDelay, bool isDead, Guid targetID)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
        this.health = health;
        this.maxHealth = maxHealth;
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;
        this.attackDelay = attackDelay;
        this.isDead = isDead;
        this.targetID = targetID;
    }

    public PlayerState(PlayerState state)
    {
        this.id = state.id;
        this.position = state.position;
        this.rotation = state.rotation;
        this.health = state.health;
        this.maxHealth = state.maxHealth;
        this.moveSpeed = state.moveSpeed;
        this.rotateSpeed = state.rotateSpeed;
        this.attackDelay = state.attackDelay;
        this.isDead = state.isDead;
        this.targetID = state.targetID;
    }
}

public struct WeaponState
{
    public Guid id;
    public float lastFired;
    public float attackSpeed;
    public float range;
    public Guid ownerID;

    public WeaponState(Guid id, float lastFired, float attackSpeed, float range, Guid ownerID)
    {
        this.id = id;
        this.lastFired = lastFired;
        this.attackSpeed = attackSpeed;
        this.range = range;
        this.ownerID = ownerID;
    }

    public WeaponState(WeaponState state)
    {
        this.id = state.id;
        this.lastFired = state.lastFired;
        this.attackSpeed = state.attackSpeed;
        this.range = state.range;
        this.ownerID = state.ownerID;
    }
}

public struct AmmoState
{
    public Guid id;
    public Vector3 position;
    public Vector3 direction;
    public Vector3 initialPosition;
    public float damage;
    public float speed;
    public float range;

    public AmmoState(Guid id, Vector3 position, Vector3 direction, Vector3 initialPosition, float damage, float speed, float range)
    {
        this.id = id;
        this.position = position;
        this.direction = direction;
        this.initialPosition = initialPosition;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
    }

    public AmmoState(AmmoState state)
    {
        this.id = state.id;
        this.position = state.position;
        this.direction = state.direction;
        this.initialPosition = state.initialPosition;
        this.damage = state.damage;
        this.speed = state.speed;
        this.range = state.range;
    }
}

public struct PlayerHitState
{
    public Guid playerID;
    public Guid ammoID;

    public PlayerHitState(Guid playerID, Guid ammoID)
    {
        this.playerID = playerID;
        this.ammoID = ammoID;
    }
}
