using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2 : MonoBehaviour
{
    //This class is for objects you want to use lookat for are a child of another lookat.
    public Transform target;
    public Quaternion parent;
    public Vector3 Axis;
    public float Speed = 1;

    void LateUpdate()
    {

        parent = Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, parent, Time.deltaTime * Speed);

        if (Axis.x == 1)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, 0f);
        }
        else if (Axis.y == 1)
        {
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
        }
        else if (Axis.z == 1)
        {
            transform.localEulerAngles = new Vector3(0f, 0f,transform.localEulerAngles.z);
        }
    }
}
