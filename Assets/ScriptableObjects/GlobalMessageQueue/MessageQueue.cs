using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalMessageQueue", menuName = "Messaging/MessageQueue")]
public class MessageQueue : ScriptableObject
{
    private Dictionary<string, Action<object>> subscribers = new Dictionary<string, Action<object>>();

    // Subscribe to a message type
    public void Subscribe(string messageType, Action<object> callback)
    {
        if (!subscribers.ContainsKey(messageType))
        {
            subscribers[messageType] = callback;
        }
        else
        {
            subscribers[messageType] += callback;
        }
    }

    // Unsubscribe from a message type
    public void Unsubscribe(string messageType, Action<object> callback)
    {
        if (subscribers.ContainsKey(messageType))
        {
            subscribers[messageType] -= callback;
            if (subscribers[messageType] == null)
            {
                subscribers.Remove(messageType);
            }
        }
    }

    // Publish a message to all subscribers
    public void Publish(string messageType, object messageData = null)
    {
        if (subscribers.ContainsKey(messageType))
        {
            subscribers[messageType]?.Invoke(messageData);
        }
    }

    // DEBUGGING: Expose Publish for Manual Calls in Editor
    [SerializeField] private string debugEventName;
    [SerializeField] private string debugEventData;

    public void DebugPublish()
    {
        Publish(debugEventName, debugEventData);
        Debug.Log($"DebugPublish - Event: {debugEventName}, Data: {debugEventData}");
    }
}
