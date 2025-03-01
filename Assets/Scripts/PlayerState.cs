using System;
using UnityEngine;
public struct PlayerState
{
    public Guid id;
    public Vector3 position;
    public Quaternion rotation;
    public float health;
    public float moveSpeed;
    public float rotateSpeed;
    public bool isDead;

    public PlayerState(PlayerState state)
    {
        this.id = state.id;
        this.position = state.position;
        this.rotation = state.rotation;
        this.health = state.health;
        this.moveSpeed = state.moveSpeed;
        this.rotateSpeed = state.rotateSpeed;
        this.isDead = state.isDead;
    }

    public PlayerState(Guid id, Vector3 position, Quaternion rotation, float health, float moveSpeed, float rotateSpeed, bool isDead)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
        this.health = health;
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;
        this.isDead = isDead;
    }
}

public struct PlayerBodyState
{
    public Guid id;
    public bool isDamaged;
    public bool isDead;

    public PlayerBodyState(PlayerBodyState state)
    {
        this.id = state.id;
        this.isDamaged = state.isDamaged;
        this.isDead = state.isDead;
    }

    public PlayerBodyState(Guid id, bool isDamaged, bool isDead)
    {
        this.id = id;
        this.isDamaged = isDamaged;
        this.isDead = isDead;
    }
}

public struct WeaponState
{
    public Guid id;
    public float attackSpeed;
    public float range;

    public WeaponState(WeaponState state)
    {
        this.id = state.id;
        this.attackSpeed = state.attackSpeed;
        this.range = state.range;
    }

    public WeaponState(Guid id, float attackSpeed, float range)
    {
        this.id = id;
        this.attackSpeed = attackSpeed;
        this.range = range;
    }
}

public struct AmmoState
{
    public Guid id;
    public float damage;
    public float speed;
    public AmmoState(AmmoState state)
    {
        this.id = state.id;
        this.damage = state.damage;
        this.speed = state.speed;
    }

    public AmmoState(Guid id, float damage, float speed)
    {
        this.id = id;
        this.damage = damage;
        this.speed = speed;
    }
}
