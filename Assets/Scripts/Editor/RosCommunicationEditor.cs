using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RosSocketCommunication))]
public class RosCommunicationEditor:Editor
{
    public override void OnInspectorGUI()
    {
        RosSocketCommunication manager = (RosSocketCommunication)target;
        DrawDefaultInspector();
        
        if (GUILayout.Button("Send msg"))
        {
            manager.SendMessage();
        }
        
    }
}