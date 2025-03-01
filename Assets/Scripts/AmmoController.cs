using System;
using UnityEngine;

public class AmmoController : MonoBehaviour, IHasGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Ammo ammo;

    Guid _id;
    public Guid id => _id;

    void OnEnable()
    {
        _id = Guid.NewGuid();
        messageQueue.Subscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);

        messageQueue.Publish(GlobalSlugs.AMMO_ADDED_TO_BATTLE, new AmmoState(id, transform.position, transform.forward, ammo.damage, ammo.speed));
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);

        messageQueue.Publish(GlobalSlugs.AMMO_REMOVED_FROM_BATTLE, id);
        _id = Guid.Empty;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var playerID = other.GetComponent<IHasGuid>().id;
            var playerHitState = new PlayerHitState(playerID, id);
            messageQueue.Publish(GlobalSlugs.PLAYER_HIT, playerHitState);
            messageQueue.Publish(GlobalSlugs.AMMO_DESPAWN, gameObject);
        }
    }

    void HandleAmmoStateChanged(object obj)
    {
        var ammo = (AmmoState)obj;
        if(ammo.id == id)
            transform.position = ammo.position;
    }
}
