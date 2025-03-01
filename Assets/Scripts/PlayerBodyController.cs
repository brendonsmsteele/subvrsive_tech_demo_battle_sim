using System;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour, IHasGuid
{
    [SerializeField] MessageQueue messageQueue;
    [SerializeField] Material deadMaterial;
    [SerializeField] Material aliveMaterial;

    public Guid id { get; set; }

    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.PLAYER_DIED, HandlePlayerDied);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.PLAYER_DIED, HandlePlayerDied);

        GetComponent<Renderer>().sharedMaterial = aliveMaterial;
    }

    void HandlePlayerDied(object obj)
    {
        var player = (PlayerState)obj;
        if (player.id == id)
            GetComponent<Renderer>().sharedMaterial = deadMaterial;
    }
}
