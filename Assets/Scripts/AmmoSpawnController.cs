using System;
using UnityEngine;

public class AmmoSpawnController : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] PrefabPooler ammoPooler;


    void Start()
    {

    }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.WEAPON_FIRED, HandleWeaponFired);
        messageQueue.Subscribe(GlobalSlugs.AMMO_DESPAWN, HandleAmmoDespawn);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_FIRED, HandleWeaponFired);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_DESPAWN, HandleAmmoDespawn);
    }

    void HandleWeaponFired(object obj)
    {
        var weapon = (WeaponState)obj;
        var ammo = ammoPooler.GetObject(weapon.ammoAnchorPosition, weapon.ammoAnchorRotation);
    }

    void HandleAmmoDespawn(object obj)
    {
        var ammo = (GameObject)obj;
        ammoPooler.ReturnObject(ammo);
    }
}
