using UnityEngine;
using System;

public class PlayerRigController: MonoBehaviour, IHasGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Character character;

    [SerializeField] Guid _id;
    public Guid id => _id;

    void OnEnable()
    {
        _id = Guid.NewGuid();
        var state = new PlayerState(_id, transform.position, transform.rotation, character.health, character.health, character.moveSpeed, character.rotateSpeed, character.attackDelay, false, Guid.Empty);
        messageQueue.Publish(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, state);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_ROTATION_CHANGED, HandlePlayerRotationChanged);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_POSITION_CHANGED, HandlePlayerPositionChanged);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_DIED, HandlePlayerDied);
    }

    void OnDisable()
    {
        messageQueue.Publish(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, _id);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_ROTATION_CHANGED, HandlePlayerRotationChanged);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_POSITION_CHANGED, HandlePlayerPositionChanged);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_DIED, HandlePlayerDied);
        _id = Guid.Empty;
    }

    void HandlePlayerRotationChanged(object obj)
    {
        var state = (PlayerState)obj;
        if(state.id == id)
            transform.rotation = state.rotation;
    }

    void HandlePlayerPositionChanged(object obj)
    {
        var state = (PlayerState)obj;
        if (state.id == id)
            transform.position = state.position;
    }

    void HandlePlayerDied(object obj)
    {
        var player = (PlayerState)obj;
        if (player.id == id)
            GetComponent<CapsuleCollider>().enabled = false;
    }
}
