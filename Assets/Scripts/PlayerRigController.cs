using UnityEngine;
using System;

public class PlayerRigController: MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Character character;

    [SerializeField] Guid _id;
    public Guid id => _id;

    void OnEnable()
    {
        _id = new Guid();
        var childrenWithGuid = GetComponentsInChildren<IHasGuid>();
        foreach (var item in childrenWithGuid)
        {
            item.id = _id;
        }
        var state = new PlayerState(_id, transform.position, transform.rotation, character.health, character.health, character.moveSpeed, character.rotateSpeed, character.attackDelay, false, Guid.Empty);
        messageQueue.Publish(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, state);
    }

    void OnDisable()
    {
        messageQueue.Publish(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, _id);
    }
}
