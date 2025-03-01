using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IHasGuid
{
    [SerializeField] MessageQueue messageQueue;
    public Guid id { get; set; }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PLAYER_HEALTH_CHANGED, HandlePlayerStateChanged);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_HEALTH_CHANGED, HandlePlayerStateChanged);
    }

    void HandlePlayerStateChanged(object obj)
    {

    }
}
