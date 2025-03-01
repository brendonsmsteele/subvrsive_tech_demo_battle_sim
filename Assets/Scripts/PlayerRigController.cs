using UnityEngine;
using System;

public class PlayerRigController: MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Character character;
    [SerializeField] PlayerHealthController healthController;
    [SerializeField] PlayerBodyController playerAnimationController;

    [SerializeField] Guid _id;
    public Guid id => _id;
    public bool isDead => healthController.isDead;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnEnable()
    {
        _id = new Guid();
        var state = new PlayerState(_id, transform.position, transform.rotation, character.health, character.moveSpeed, character.rotateSpeed, character.attackDelay, false, Guid.Empty);
        messageQueue.Publish(GlobalSlugs.PLAYER_ADDED_TO_BATTLE, state);
    }

    void OnDisable()
    {
        messageQueue.Publish(GlobalSlugs.PLAYER_REMOVED_FROM_BATTLE, _id);
    }
}
