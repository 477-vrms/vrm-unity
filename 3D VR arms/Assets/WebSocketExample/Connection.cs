using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class Connection : MonoBehaviour
{
    
    WebSocket websocket;
    //int count;
    public Transform Joint1;
    public Transform Joint2;
    public Transform Joint3;
    public Transform Joint4;
    public Transform Joint5;
    public Transform Joint6;
    public Transform Joint7;
    public TriggerGrip Gripper; //gets grip % from a custom script (Grip Percent game object)
    public class MyClass
    {
        public float Joint1;
        public float Joint2;
        public float Joint3;
        public float Joint4;
        public float Joint5;
        public float Joint6;
        public float Joint7;
        public float Joint8;
    }

    // Start is called before the first frame update
    async void Start()
    {
        //count = 0;
        // websocket = new WebSocket("ws://echo.websocket.org");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer SENIORDESIGNMADEMEDOTHIS");
        headers.Add("Content-Type", "application/json");
        websocket = new WebSocket("wss://ecess-api.matthewwen.com/vrms/joints/person", headers);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            // Reading a plain text message
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
        };

        await websocket.Connect();
        if (websocket.State == WebSocketState.Open)
        {
            Debug.Log("It is Open");
        }
        else
        {
            Debug.Log("Connection: " + websocket.State);
        }

    }

    async void Update()
    {
            #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
            #endif
            if (websocket.State == WebSocketState.Open)
            {
                MyClass myObject = new MyClass();
                myObject.Joint1 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)).y;
                myObject.Joint2 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint2.transform)).x;
                myObject.Joint3 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint3.transform)).x;
                myObject.Joint4 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint4.transform)).y;
                myObject.Joint5 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint5.transform)).x;
                myObject.Joint6 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint6.transform)).x;
                myObject.Joint7 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint7.transform)).z;
                myObject.Joint8 = Gripper.getGrip();

                //string json = ;
                await websocket.SendText(JsonUtility.ToJson(myObject));
                
            }
            else
            {
                Debug.Log("Connection closed.");
            }
        
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
