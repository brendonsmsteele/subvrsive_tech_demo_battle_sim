using UnityEngine;
using System;

public class PlayerRigController: MonoBehaviour, IHasGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Character character;

    [SerializeField] Guid _id;
    Guid IHasGuid.id => _id;

    void OnEnable()
    {
        _id = new Guid();
        var state = new PlayerState(_id, transform.position, transform.rotation, character.health, character.health, character.moveSpeed, character.rotateSpeed, character.attackDelay, false, _id);
        messageQueue.Publish(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, state);
    }

    void OnDisable()
    {
        messageQueue.Publish(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, _id);
    }
}
