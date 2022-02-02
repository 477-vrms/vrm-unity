using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRotation : MonoBehaviour
{
    public Vector3 Limit;
    public Material good;
    public Material BAD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angleX = (transform.localEulerAngles.x > 180) ? transform.localEulerAngles.x - 360 : transform.localEulerAngles.x;
        float angleY = (transform.localEulerAngles.y > 180) ? transform.localEulerAngles.y - 360 : transform.localEulerAngles.y;
        float angleZ = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
        transform.localEulerAngles = (new Vector3(Mathf.Clamp(angleX, -Limit.x, Limit.x), Mathf.Clamp(angleY, -Limit.y, Limit.y), Mathf.Clamp(angleZ, -Limit.z, Limit.z)));
        if(Mathf.Abs(angleX) > Limit.x-1 || Mathf.Abs(angleY) > Limit.y-1 || Mathf.Abs(angleZ) > Limit.z-1)
        {
            gameObject.GetComponent<MeshRenderer>().material = BAD;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = good;
        }
        //Transform.rotation = Mathf.Max(Transform.rotation, Limit);
        //Transform.rotation = Mathf.Min(Transform.rotation, Limit);
    }
}
