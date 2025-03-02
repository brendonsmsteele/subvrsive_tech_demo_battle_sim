using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class AmmoController : MonoBehaviour, IHasGuid, IHasParentGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Ammo ammo;

    Guid _id;
    public Guid id => _id;
    public Guid parentID { get; set; }


    void OnEnable()
    {
        _id = Guid.NewGuid();
        messageQueue.Subscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);
        messageQueue.Subscribe(GlobalSlugs.AMMO_DESPAWN, HandleAmmoDespawn);
        messageQueue.Publish(GlobalSlugs.AMMO_ADDED_TO_BATTLE, new AmmoState(id, transform.position, transform.forward, transform.position, ammo.damage, ammo.speed, parentID));
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_DESPAWN, HandleAmmoDespawn);
        messageQueue.Publish(GlobalSlugs.AMMO_REMOVED_FROM_BATTLE, id);
        parentID = Guid.Empty;
        _id = Guid.Empty;
    }

    void OnTriggerEnter(Collider other)
    {
        if(id == Guid.Empty)
        {
            Debug.Log("A phantom hit - probably due to ammo hitting two colliders in the same frame, thus triggering a second time after has returned to the pool.");
        }
        else if (other.tag == "Player")
        {
            var playerID = other.GetComponent<PlayerRigController>().id;
            var playerHitState = new PlayerHitState(playerID, id);
            messageQueue.Publish(GlobalSlugs.PLAYER_HIT, playerHitState);
        }
        AmmoFactory.Instance.ReturnObject(this.gameObject);
    }

    void HandleAmmoStateChanged(object obj)
    {
        var ammo = (AmmoState)obj;
        if(ammo.id == id)
            transform.position = ammo.position;
    }

    void HandleAmmoDespawn(object obj)
    {
        var ammo = (AmmoState)obj;
        if(ammo.id == id)
            AmmoFactory.Instance.ReturnObject(this.gameObject);
    }
}
