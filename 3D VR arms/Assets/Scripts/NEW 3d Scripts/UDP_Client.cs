//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using UnityEngine;
//public class UDP_Client : MonoBehaviour
//{

//    void Start()
//    {

//        UDPTest();
//    }
//    void Update()
//    {
//        //UDPTest();
//    }
//    void UDPTest()
//    {


//        UdpClient client = new UdpClient();
//        //IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5600);

//        //IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 2002);
//        try
//        {
//            //Uri myUri = new Uri("34.132.95.250", UriKind.Absolute);

//            //IPAddress ipAddress = Dns.Resolve("34.132.95.250").AddressList[0];
//            IPEndPoint server = new IPEndPoint(IPAddress.Parse("34.132.95.250"), 2002);
//            //client.Connect(server);
//            //client.Client.Blocking = false;
//            client.Client.ReceiveTimeout = 1000;
//            byte[] sendBytes = Encoding.ASCII.GetBytes("Hello, from the client");
//            //client.Send(new byte[] { 1, 2, 3, 4, 5 }, 5);
//            client.Send(sendBytes, sendBytes.Length, server);
//            byte[] receiveBytes = client.Receive(ref server);
//            string receivedString = Encoding.ASCII.GetString(receiveBytes); 
//            Debug.Log("Message received from the server \n " + receivedString);


//        }
//        catch (Exception e)
//        {
//            print("Exception thrown " + e.Message);
//        }
//    }
//}
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Text;
using UnityEngine;
//using Unity.Collections.NativeArray;
using System.IO;
using UnityEngine.UI;

public class UDP_Client : MonoBehaviour
{
    public RawImage YourRawImage;
    public RawImage HandImage;
    bool excep = false;
    //private Texture2D tex = new Texture2D(640, 480, TextureFormat.PVRTC_RGBA4, false);
    public Texture2D test;
    void Start()
    {
        UdpClient client = new UdpClient(5600);
        StartCoroutine(waiter(client));
    }
    IEnumerator waiter(UdpClient client)
    {

        Texture2D tex = new Texture2D(320, 240);
        client.Connect("34.132.95.250", 2002);
        byte[] sendBytes = Encoding.ASCII.GetBytes("{\"id\": \"vrms_unity\"}");
        client.Send(sendBytes, sendBytes.Length);
        while (true)
        {
            try
            {

                
                client.Client.ReceiveTimeout = 1000;
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
                byte[] receiveBytes = client.Receive(ref remoteEndPoint);
                //string receivedString = Encoding.ASCII.GetByteCount(receiveBytes);
                //print("Message received from the server \n " + receiveBytes.Length);
                //tex = receiveBytes.texture;
                //bytes = receiveBytes.EncodeToPNG();
                //var texturee = new Texture2D(2, 2);
                //print("load");
                //File.WriteAllBytes("Assets/temp2.txt", receiveBytes);
                //Debug.Log(System.Text.Encoding.ASCII.GetString(receiveBytes));
                //tex = receiveBytes.texture;
                //receiveBytes = tex.EncodeToPNG();
                tex.LoadImage(receiveBytes);
                tex.Apply();
                //print("set tex");
                YourRawImage.texture = tex;
                HandImage.texture = tex;

            }

            catch (Exception e)
            {
                print("Exception thrown " + e.Message);
                excep = true;
            }
            //Wait for 5 seconds
            if(excep == true)
            {
                excep = false;
                yield return new WaitForSeconds(15);
                
                client.Send(sendBytes, sendBytes.Length);
            }
            else
            {
                yield return new WaitForSeconds(0.03F); //0.03 = 30 frames?
            }
            //TextAsset bindata = test;
            //var test2 = test.GetRawTextureData();
            //tex.LoadRawTextureData(test2);
            //print("set tex");
            //YourRawImage.texture = tex;
            

        }


    }
    void UDPTest()
    {
        UdpClient client = new UdpClient(5600);
        try
        {
            client.Connect("34.132.95.250", 2002);
            byte[] sendBytes = Encoding.ASCII.GetBytes("{\"id\": \"vrms_unity\"}");
            client.Send(sendBytes, sendBytes.Length);

            client.Client.ReceiveTimeout = 1000;
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
            byte[] receiveBytes = client.Receive(ref remoteEndPoint);
            string receivedString = Encoding.ASCII.GetString(receiveBytes);
            print("Message received from the server \n " + receivedString);
        }
        catch (Exception e)
        {
            print("Exception thrown " + e.Message);
        }
    }
}