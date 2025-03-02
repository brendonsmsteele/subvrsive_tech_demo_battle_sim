using System;
using UnityEngine;

public class WeaponController : MonoBehaviour, IHasParentGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Weapon weapon;
    [SerializeField] Transform ammoAnchor;

    Guid _id;
    Guid _parentID;
    public Guid parentID => _parentID;

    void OnEnable()
    {
        _id = Guid.NewGuid();
        _parentID = GetComponentInParent<IHasGuid>().id;

        messageQueue.Subscribe(GlobalSlugs.WEAPON_FIRED, HandleWeaponFired);
        messageQueue.Subscribe(GlobalSlugs.AMMO_DESPAWN, HandleAmmoDespawn);
        messageQueue.Publish(GlobalSlugs.WEAPON_ADDED_TO_BATTLE, new WeaponState(_id, 0f, weapon.attackSpeed, weapon.range, ammoAnchor.position, ammoAnchor.rotation, parentID));
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_FIRED, HandleWeaponFired);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_DESPAWN, HandleAmmoDespawn);

        messageQueue.Publish(GlobalSlugs.WEAPON_REMOVED_FROM_BATTLE, _id);
        _parentID = Guid.Empty;
        _id = Guid.Empty;
    }

    void HandleWeaponFired(object obj)
    {
        var weapon = (WeaponState)obj;
        if(weapon.id == _id)
        {
            //var weapon = (WeaponState)obj;
            //var ammo = AmmoFactory.Instance.GetObject(weapon.ammoAnchorPosition, weapon.ammoAnchorRotation);
        }
    }

    void HandleAmmoDespawn(object obj)
    {

        //var ammo = (GameObject)obj;
        //ammoPooler.ReturnObject(ammo);
    }
}
