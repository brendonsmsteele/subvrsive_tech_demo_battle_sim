#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MessageQueue))]
public class MessageQueueEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draw default fields

        MessageQueue messageQueue = (MessageQueue)target;
        if (GUILayout.Button("Publish Debug Event"))
        {
            messageQueue.DebugPublish();
        }
    }
}
#endif