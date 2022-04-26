using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class Connection : MonoBehaviour
{
    
    WebSocket websocket;
    //int count;
    public int offset1 = 0;
    public bool flip1;
    
    public int offset2 = 0;
    public bool flip2;
    
    public int offset3 = 0;
    public bool flip3;
   
    public int offset4 = 0;
    public bool flip4;
    
    public int offset5 = 0;
    public bool flip5;
    
    public int offset6 = 0;
    public bool flip6;
    
    public int offset7 = 0;
    public bool flip7;
    public Transform Joint1;
    public Transform Joint2;
    public Transform Joint3;
    public Transform Joint4;
    public Transform Joint5;
    public Transform Joint6;
    public Transform Joint7;
    public TriggerGrip Gripper; //gets grip % from a custom script (Grip Percent game object)
    public int rate = 0;
    System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
    public class MyClass
    {
        public int J1;
        public int J2;
        public int J3;
        public int J4;
        public int J5;
        public int J6;
        public int J7;
        public int J8;
        public string T;
        public string action;
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
        StartCoroutine(waiter());
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

    IEnumerator waiter()
    {
        while(true)
        {
            //Debug.Log("ello");

#if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
#endif
            double time = (System.DateTime.UtcNow - epochStart).TotalSeconds;
            //Debug.Log(time);
            //Debug.Log((int)(time*10 % 10));
            if (websocket.State == WebSocketState.Open)// && (int)(time*1000 % 10) == 0)
            {

                MyClass myObject = new MyClass();
                myObject.J1 = (1 - 2 * Convert.ToInt32(flip1)) * Account(Joint1.transform.localEulerAngles, "y") + 180 + offset1;
                myObject.J2 = (1 - 2 * Convert.ToInt32(flip2)) * Account(Joint2.transform.localEulerAngles, "x") + 180 + offset2;
                myObject.J3 = (1 - 2 * Convert.ToInt32(flip3)) * Account(Joint3.transform.localEulerAngles, "x") + offset3;
                myObject.J4 = (1 - 2 * Convert.ToInt32(flip4)) * Account(Joint4.transform.localEulerAngles, "y") + offset4;
                myObject.J5 = (1 - 2 * Convert.ToInt32(flip5)) * Account(Joint5.transform.localEulerAngles, "y") + 180 + offset5;
                myObject.J6 = (1 - 2 * Convert.ToInt32(flip6)) * Account(Joint6.transform.localEulerAngles, "y") + 180 + offset6;
                myObject.J7 = (1 - 2 * Convert.ToInt32(flip7)) * Account(Joint7.transform.localEulerAngles, "z") + 180 + offset7;
                myObject.J8 = (int)Gripper.getGrip();


                //double cur_time = (time);
                myObject.T = (time.ToString("F2"));
                myObject.action = ("move");
                //Debug.Log(myObject.T);
                //string json = ;
                //print("hello");
                websocket.SendText(JsonUtility.ToJson(myObject));
                //Debug.Log(JsonUtility.ToJson(myObject));

            }
            else
            {
                //Debug.Log("Connection closed.");
            }
            //count++; //for slowing down update speed

            yield return new WaitForSeconds(0.1F);
            
            
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
    public int Account(Vector3 angle, string axis)
    {
        float newAngle = 0;

        if (axis == "x")
        {
            newAngle = angle.x;
            if (angle.y == 180 && angle.z == 180)
            {
                newAngle = (360 - angle.x + 180) % 360;
            }
            else if (angle.y == 0 && angle.z == 0)
            {
                if (angle.x > 180)
                {
                    newAngle = angle.x - 360;
                }
            }

        }
        else if (axis == "y")
        {
            newAngle = angle.y;
            if (angle.x == 180 && angle.z == 180)
            {
                newAngle = (360 - angle.y + 180) % 360;
            }
            else if (angle.x == 0 && angle.z == 0)
            {
                if (angle.y > 180)
                {
                    newAngle = angle.y - 360;
                }
            }

        }
        else if (axis == "z")
        {
            newAngle = angle.z;
            if (angle.x == 180 && angle.y == 180)
            {
                newAngle = (360 - angle.z + 180) % 360;
            }
            else if (angle.x == 0 && angle.y == 0)
            {
                if (angle.z > 180)
                {
                    newAngle = angle.z - 360;
                }
            }

        }

        return (int)newAngle;
    }

}

