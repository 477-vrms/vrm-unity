
////using System;
////using System.Net;
////using System.Net.Sockets;
////using System.Collections;
////using System.Text;
////using UnityEngine;
//////using Unity.Collections.NativeArray;
////using System.IO;
////using UnityEngine.UI;

////public class UDP_Client : MonoBehaviour
////{
////    public RawImage YourRawImage;
////    public RawImage HandImage;
////    bool excep = false;
////    //private Texture2D tex = new Texture2D(640, 480, TextureFormat.PVRTC_RGBA4, false);
////    public Texture2D test;
////    void Start()
////    {
////        UdpClient client = new UdpClient(5600);
////        StartCoroutine(waiter(client));
////    }
////    IEnumerator waiter(UdpClient client)
////    {

////        Texture2D tex = new Texture2D(320, 240);
////        client.Connect("34.132.95.250", 2002);
////        byte[] sendBytes = Encoding.ASCII.GetBytes("{\"id\": \"vrms_unity\"}");
////        client.Send(sendBytes, sendBytes.Length);
////        while (true)
////        {
////            try
////            {


////                client.Client.ReceiveTimeout = 1000;
////                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
////                byte[] receiveBytes = client.Receive(ref remoteEndPoint);
////                //string receivedString = Encoding.ASCII.GetByteCount(receiveBytes);
////                //print("Message received from the server \n " + receiveBytes.Length);
////                //tex = receiveBytes.texture;
////                //bytes = receiveBytes.EncodeToPNG();
////                //var texturee = new Texture2D(2, 2);
////                //print("load");
////                //File.WriteAllBytes("Assets/temp2.txt", receiveBytes);
////                //Debug.Log(System.Text.Encoding.ASCII.GetString(receiveBytes));
////                //tex = receiveBytes.texture;
////                //receiveBytes = tex.EncodeToPNG();
////                tex.LoadImage(receiveBytes);
////                tex.Apply();
////                //print("set tex");
////                YourRawImage.texture = tex;
////                HandImage.texture = tex;

////            }

////            catch (Exception e)
////            {
////                print("Exception thrown " + e.Message);
////                excep = true;
////            }
////            //Wait for 5 seconds
////            if(excep == true)
////            {
////                excep = false;
////                yield return new WaitForSeconds(15);

////                client.Send(sendBytes, sendBytes.Length);
////            }
////            else
////            {
////                yield return new WaitForSeconds(0.03F); //0.03 = 30 frames?
////            }
////            //TextAsset bindata = test;
////            //var test2 = test.GetRawTextureData();
////            //tex.LoadRawTextureData(test2);
////            //print("set tex");
////            //YourRawImage.texture = tex;


////        }


////    }
////    void UDPTest()
////    {
////        UdpClient client = new UdpClient(5600);
////        try
////        {
////            client.Connect("34.132.95.250", 2002);
////            byte[] sendBytes = Encoding.ASCII.GetBytes("{\"id\": \"vrms_unity\"}");
////            client.Send(sendBytes, sendBytes.Length);

////            client.Client.ReceiveTimeout = 1000;
////            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
////            byte[] receiveBytes = client.Receive(ref remoteEndPoint);
////            string receivedString = Encoding.ASCII.GetString(receiveBytes);
////            print("Message received from the server \n " + receivedString);
////        }
////        catch (Exception e)
////        {
////            print("Exception thrown " + e.Message);
////        }
////    }
////}
//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Collections;
//using System.Text;
//using UnityEngine;
//using System.IO;
//using UnityEngine.UI;

//public class UDP_Client : MonoBehaviour
//{
//    public RawImage YourRawImage;
//    public RawImage Handheld;
//    UdpClient client = null;
//    IPEndPoint remoteEndPoint = null;

//    const int packet_size = 25000;
//    const int header_size = 10;
//    const int data_packet_size = packet_size - header_size;

//    int img_id = -1;
//    int num_packets_expected = -1;
//    int num_packets_recieved = -1;
//    private byte[] bytes = null;
//    void Start()
//    {
//        this.client = new UdpClient(5600);
//        try
//        {
//            this.client.Connect("34.132.95.250", 2002);
//            this.client.Client.ReceiveTimeout = 100000;
//            this.remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
//            StartCoroutine(waiter());
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }


//    }

//    void init_camera()
//    {
//        byte[] sendBytes = Encoding.ASCII.GetBytes("{\"id\": \"vrms_unity\"}");
//        client.Send(sendBytes, sendBytes.Length);
//    }

//    void add_packet(byte[] packet, Texture2D tex)
//    {
//        int photo_id = BitConverter.ToInt32(packet, 0);
//        int packet_id = packet[4];
//        int num_packets = packet[5];
//        int photo_size = BitConverter.ToInt32(packet, 6);
//        // print("photo id: " + photo_id + ", packet id: " + packet_id + ", num packets: " + num_packets + ", photo size: " + photo_size);
//        if (photo_id != this.img_id)
//        {
//            this.img_id = photo_id;
//            this.bytes = new byte[photo_size];
//            this.num_packets_expected = num_packets;
//            this.num_packets_recieved = 0;
//        }
//        int byte_idx = data_packet_size * packet_id;
//        for (int i = 10; i < packet.Length; i++)
//        {
//            this.bytes[byte_idx] = packet[i];
//            byte_idx += 1;
//        }
//        this.num_packets_recieved += 1;
//        if (this.num_packets_expected == this.num_packets_recieved)
//        {
//            tex.LoadImage(this.bytes);
//            tex.Apply();
//            YourRawImage.texture = tex;
//            Handheld.texture = tex;
//        }
//    }

//    IEnumerator waiter()
//    {
//        Texture2D tex = new Texture2D(640, 480);
//        int count = 0;
//        this.init_camera();
//        while (true)
//        {
//            count += 1;
//            Exception exp = null;
//            try
//            {
//                //print("1");
//                //if(this.client.Available >0)
//                //{
//                    byte[] packet = this.client.Receive(ref remoteEndPoint);
//                    //print("2");
//                    add_packet(packet, tex);

//                //}
//                //print("3");

//            }
//            catch (Exception e)
//            {
//                exp = e;
//                print("Exception thrown: " + e.Message);
//            }
//            if (exp != null)
//            {
//                //print("3");
//                yield return new WaitForSeconds(1);
//                //print("4");
//                this.init_camera();
//            }
//            else
//            {
//                yield return new WaitForSeconds(0.03F); //0.03 = 30 frames?
//            }
//        }

//    }
//}
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Text;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class UDP_Client : MonoBehaviour
{
    public RawImage YourRawImage;
    public RawImage YourRawImage2;
    UdpClient client = null;
    IPEndPoint remoteEndPoint = null;

    const int packet_size = 25000;
    const int header_size = 10;
    const int data_packet_size = packet_size - header_size;

    int img_id = -1;
    int num_packets_expected = -1;
    int num_packets_recieved = -1;
    private byte[] bytes = null;
    void Start()
    {
        this.client = new UdpClient(5600);
        this.client.Connect("34.132.95.250", 2002);
        this.client.Client.ReceiveTimeout = 1000;
        this.remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
        StartCoroutine(waiter());
    }

    void init_camera()
    {
        byte[] sendBytes = Encoding.ASCII.GetBytes("{\"id\": \"vrms_unity\"}");
        client.Send(sendBytes, sendBytes.Length);
    }

    void add_packet(byte[] packet, Texture2D tex)
    {
        int photo_id = BitConverter.ToInt32(packet, 0);
        int packet_id = packet[4];
        int num_packets = packet[5];
        int photo_size = BitConverter.ToInt32(packet, 6);
        // print("photo id: " + photo_id + ", packet id: " + packet_id + ", num packets: " + num_packets + ", photo size: " + photo_size);
        if (photo_id != this.img_id)
        {
            this.img_id = photo_id;
            this.bytes = new byte[photo_size];
            this.num_packets_expected = num_packets;
            this.num_packets_recieved = 0;
        }
        int byte_idx = data_packet_size * packet_id;
        for (int i = 10; i < packet.Length; i++)
        {
            this.bytes[byte_idx] = packet[i];
            byte_idx += 1;
        }
        this.num_packets_recieved += 1;
        if (this.num_packets_expected == this.num_packets_recieved)
        {
            tex.LoadImage(this.bytes);
            tex.Apply();
            YourRawImage.texture = tex;
            YourRawImage2.texture = tex;
        }
    }

    IEnumerator waiter()
    {
        Texture2D tex = new Texture2D(640, 480);
        int count = 0;
        this.init_camera();
        while (true)
        {
            count += 1;
            Exception exp = null;
            try
            {
                byte[] packet = this.client.Receive(ref remoteEndPoint);
                add_packet(packet, tex);
            }
            catch (Exception e)
            {
                exp = e;
                print("Exception thrown: " + e.Message);
            }
            if (exp != null)
            {
                yield return new WaitForSeconds(5);
                this.init_camera();
            }
            else
            {
                yield return new WaitForSeconds(0.05F); //0.03 = 30 frames?
            }
        }

    }
}