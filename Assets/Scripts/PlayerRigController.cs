using UnityEngine;
using System;

public class PlayerRigController: MonoBehaviour, IHasGuid, IHasPrefabPooler
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Character character;

    [SerializeField] Guid _id;
    public Guid id => _id;
    public PrefabPooler pool { get; set; }

    void OnEnable()
    {
        _id = Guid.NewGuid();
        var state = new PlayerState(_id, transform.position, transform.rotation, character.health, character.health, character.moveSpeed, character.rotateSpeed, character.attackDelay, false, Guid.Empty);
        messageQueue.Publish(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, state);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_ROTATION_CHANGED, HandlePlayerRotationChanged);
        messageQueue.Subscribe(GlobalSlugs.PLAYER_POSITION_CHANGED, HandlePlayerPositionChanged);
    }

    void OnDisable()
    {
        messageQueue.Publish(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, _id);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_ROTATION_CHANGED, HandlePlayerRotationChanged);
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_POSITION_CHANGED, HandlePlayerPositionChanged);
        pool = null;
        _id = Guid.Empty;
    }

    void HandlePlayerRotationChanged(object obj)
    {
        var state = (PlayerState)obj;
        transform.rotation = state.rotation;
    }

    void HandlePlayerPositionChanged(object obj)
    {
        var state = (PlayerState)obj;
        transform.position = state.position;
    }
}
