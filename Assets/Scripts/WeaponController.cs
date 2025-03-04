using System;
using UnityEngine;

public class WeaponController : MonoBehaviour, IHasParentGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Weapon weapon;
    [SerializeField] Transform ammoAnchor;

    Guid _id;
    public Guid parentID { get; set; }

    void OnEnable()
    {
        _id = Guid.NewGuid();
        parentID = GetComponentInParent<IHasGuid>().id;
        messageQueue.Subscribe(GlobalSlugs.WEAPON_FIRED, HandleWeaponFired);
        messageQueue.Publish(GlobalSlugs.WEAPON_ADDED_TO_BATTLE, new WeaponState(_id, 0f, weapon.attackSpeed, weapon.range, parentID));
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.WEAPON_FIRED, HandleWeaponFired);
        messageQueue.Publish(GlobalSlugs.WEAPON_REMOVED_FROM_BATTLE, _id);
        parentID = Guid.Empty;
        _id = Guid.Empty;
    }

    void HandleWeaponFired(object obj)
    {
        var weapon = (WeaponState)obj;
        if(weapon.id == _id)
        {
            AmmoController ammo = AmmoFactory.Instance.GetObject(ammoAnchor.position, ammoAnchor.rotation, false);
            ammo.parentID = _id;
            ammo.gameObject.SetActive(true);
        }
    }
}
