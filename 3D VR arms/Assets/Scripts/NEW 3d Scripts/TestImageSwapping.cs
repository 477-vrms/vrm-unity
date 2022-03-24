using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TestImageSwapping : MonoBehaviour
{
    public RawImage YourRawImage;
    private int count = 1;
    private bool Brian = true;
    private Texture2D tex = new Texture2D(16, 16, TextureFormat.PVRTC_RGBA4, false);
    private Texture2D tex2 = new Texture2D(16, 16, TextureFormat.PVRTC_RGBA4, false);
    private byte[] bytes = null;
    private byte[] bytesB = null;
    private byte[] bytesM = null;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DownloadImage("https://engineering.purdue.edu/477grp2/Team/img/BrianLatimer.jpg"));
        StartCoroutine(DownloadImage2("https://engineering.purdue.edu/477grp2/Team/img/BrianLatimer.jpg"));
    }


    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //YourRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            bytes = tex.EncodeToPNG();
            tex.LoadImage(bytes);
            YourRawImage.texture = tex;
        }
            
    }

    IEnumerator DownloadImage2(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://engineering.purdue.edu/477grp2/Team/img/BrianLatimer.jpg");
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            bytesB = tex.EncodeToPNG();
        }
        UnityWebRequest request2 = UnityWebRequestTexture.GetTexture("https://engineering.purdue.edu/477grp2/Team/img/matthewwen.jpg");
        yield return request2.SendWebRequest();
        if (request2.isNetworkError || request2.isHttpError)
            Debug.Log(request2.error);
        else
        {
            tex = ((DownloadHandlerTexture)request2.downloadHandler).texture;
            bytesM = tex.EncodeToPNG();
        }
        bytes = bytesB;
        if (bytesB != null && bytesM != null)
        {
            while (true)
            {


                yield return tex.LoadImage(bytes);
                YourRawImage.texture = tex;

                if (Brian == true)
                {
                    //MediaUrl = "https://engineering.purdue.edu/477grp2/Team/img/matthewwen.jpg";
                    bytes = bytesM;
                    Brian = false;
                }
                else
                {
                    //MediaUrl = "https://engineering.purdue.edu/477grp2/Team/img/BrianLatimer.jpg";
                    bytes = bytesB;
                    Brian = true;
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        //if(count == 1)
        //{
        //    StartCoroutine(DownloadImage("https://engineering.purdue.edu/477grp2/Team/img/BrianLatimer.jpg"));
        //}
        //else if(count == 10)
        //{
        //    StartCoroutine(DownloadImage("https://engineering.purdue.edu/477grp2/Team/img/matthewwen.jpg"));
        //}
        //else
        //{
        //    count++;
        //    if (count > 20)
        //    {
        //        count = 1;
        //    }
        //}
        
    }
}
