using System;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Ammo ammo;

    Guid id = Guid.NewGuid();
    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);

        messageQueue.Publish(GlobalSlugs.AMMO_ADDED_TO_BATTLE, new AmmoState(id, transform.position, transform.forward, ammo.damage, ammo.speed));
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.AMMO_STATE_CHANGED, HandleAmmoStateChanged);

        messageQueue.Publish(GlobalSlugs.AMMO_REMOVED_FROM_BATTLE, new AmmoState(id, transform.position, transform.forward, ammo.damage, ammo.speed));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var playerID = other.GetComponent<IHasGuid>().id;
            var playerHitState = new PlayerHitState(playerID, id);
            messageQueue.Publish(GlobalSlugs.PLAYER_HIT, playerHitState);
        }
    }

    void HandleAmmoStateChanged(object obj)
    {
        var ammo = (AmmoState)obj;
        if(ammo.id == id)
            transform.position = ammo.position;
    }
}
