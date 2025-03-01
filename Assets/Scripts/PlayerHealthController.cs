using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IHasParentGuid
{
    [SerializeField] MessageQueue messageQueue;
    Guid _parentID;
    public Guid parentID => _parentID;

    void OnEnable()
    {
        _parentID = GetComponentInParent<IHasGuid>().id;
        messageQueue.Subscribe(GlobalSlugs.PLAYER_HEALTH_CHANGED, HandlePlayerStateChanged);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_HEALTH_CHANGED, HandlePlayerStateChanged);
        _parentID = Guid.Empty;
    }

    void HandlePlayerStateChanged(object obj)
    {
    }
}
