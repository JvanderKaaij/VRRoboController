using System;
using System.Net;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using NativeWebSocket;

[Serializable]
public class TimeMsg
{
    public int sec = 9000;
    public int nsec = 0;
}

[Serializable]
public class HeaderMsg
{
    public TimeMsg stamp = new TimeMsg();
    public string frame_id = "map";
}
[Serializable]
public class PoseMsg
{
    public Vector3 position = new Vector3(0f,0f,0f);
    public Quaternion orientation = new Quaternion(0,0,0,0);
}
[Serializable]
public class FullMsg
{
    public HeaderMsg header = new HeaderMsg();
    public PoseMsg pose = new PoseMsg();
}

[Serializable]
public class GoalPose
{
    public string op = "publish";
    public string id = "publish:/goal_pose:39";
    public string topic= "/goal_pose";
    public FullMsg msg = new FullMsg();
    public bool latch = false;
}

public class RosSocketCommunication : MonoBehaviour
{
    public string address;
    public int port = 9090;
    public Vector3 targetPosition;
    public ConnectionIndicator idicator;
    
    WebSocket websocket;
    private Socket sender;
    async private void Connect()
    {
        Debug.Log("PassThrough: Trying Connection");
        websocket = new WebSocket(String.Format("ws://{0}:{1}", address, port));
        
        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            idicator.SetConnected();
        };
        
        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };
        
        await websocket.Connect();
    }
    
    public async void SendMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            var goalPoseMsg = new GoalPose();
            goalPoseMsg.msg.pose.position = targetPosition;
            string jsonString = JsonUtility.ToJson(goalPoseMsg);
            await websocket.SendText(jsonString);
        }
    }
    
    public async void SendPosition(Vector3 pos)
    {
        if (websocket.State == WebSocketState.Open)
        {
            var goalPoseMsg = new GoalPose();
            goalPoseMsg.msg.pose.position = pos;
            string jsonString = JsonUtility.ToJson(goalPoseMsg);
            await websocket.SendText(jsonString);
        }
    }

    private async void OnDestroy()
    {
        await websocket.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PassThrough: Start");
        Connect();
    }

}
