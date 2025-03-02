using System;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour, IHasParentGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Material deadMaterial;
    [SerializeField] Material aliveMaterial;

    public Guid parentID { get; set; }

    void OnEnable()
    {
        parentID = GetComponentInParent<IHasGuid>().id;
        GetComponentInChildren<Renderer>().sharedMaterial = aliveMaterial;
        messageQueue.Subscribe(GlobalSlugs.PLAYER_DIED, HandlePlayerDied);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_DIED, HandlePlayerDied);
        parentID = Guid.Empty;
    }

    void HandlePlayerDied(object obj)
    {
        var player = (PlayerState)obj;
        if (player.id == parentID)
            GetComponentInChildren<Renderer>().sharedMaterial = deadMaterial;
    }
}
