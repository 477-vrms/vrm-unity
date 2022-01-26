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
    private float nextActionTime = 0.0f;
    public float period = 0.1f;

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
            Debug.Log("It is Closed" + websocket.State);
        }

    }

    async void Update()
    {
        
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            Debug.Log("Update Call");
            #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
            #endif
            if (websocket.State == WebSocketState.Open)
            {
                Debug.Log("It is Open");
                MyClass myObject = new MyClass();
                myObject.Joint1 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)).y;
                myObject.Joint2 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint2.transform)).x;
                myObject.Joint3 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint3.transform)).x;
                myObject.Joint4 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint4.transform)).y;
                myObject.Joint5 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint5.transform)).x;
                myObject.Joint6 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint6.transform)).x;
                myObject.Joint7 = (UnityEditor.TransformUtils.GetInspectorRotation(Joint7.transform)).z;
                

                //GRIPPER SECTION
                var inputDevices = new List<UnityEngine.XR.InputDevice>();
                UnityEngine.XR.InputDevices.GetDevices(inputDevices);
                float triggerFloatL = 0;
                float triggerFloatR = 0;
                float triggerFloat = 0;

                foreach (var device in inputDevices)
                {
                    //gameObject.GetComponent<TextMesh>().text += (string.Format("\nDevice found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
                    if (device.role.ToString() == "LeftHanded")
                    {
                        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatL);
                    }
                    if (device.role.ToString() == "RightHanded")
                    {
                        device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatR);
                    }
                }
                triggerFloat = Mathf.Max(triggerFloatL, triggerFloatR);
                if (triggerFloat < 0.01)
                {
                    //gameObject.GetComponent<TextMesh>().text += "\nClaw: 0%";
                    myObject.Joint8 = 0;
                }
                else
                {
                    myObject.Joint8 =  (Mathf.Round(triggerFloat * 100f) / 100f) * 100;
                }
                //END GRIPPER SECTION

                //string json = ;
                await websocket.SendText(JsonUtility.ToJson(myObject));
                
            }
            else
            {
                Debug.Log("It is close");
            }
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
