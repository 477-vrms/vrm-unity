using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public Vector3 Axis;
    public float MinAngle;
    public float MaxAngle;
    //float lockPos = 0;

    void Update()
    {
        


        //var lookPos = target.position - transform.position;
        //if (Axis.x == 1)
        //{
        //    Vector3 eulerRotation = new Vector3(Mathf.Clamp(target.transform.eulerAngles.x, MinAngle, MaxAngle), transform.eulerAngles.y, transform.eulerAngles.z);

        //    transform.rotation = Quaternion.Euler(eulerRotation);
        //}
        //else if (Axis.y == 1)
        //{
        //    Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, Mathf.Clamp(target.transform.eulerAngles.y,MinAngle,MaxAngle), transform.eulerAngles.z);

        //    transform.rotation = Quaternion.Euler(eulerRotation);
        //}
        //else if (Axis.z == 1)
        //{
        //    Vector3 eulerRotation = new Vector3(transform.localRotation.x, transform.localRotation.y, Mathf.Clamp(target.transform.eulerAngles.z, MinAngle, MaxAngle));

        //    transform.localRotation = Quaternion.Euler(eulerRotation);
        //    transform.localRotation = new Vector3(0,0,50);
        //}
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.LocalRotation, rotation, Time.deltaTime * Speed);
    }
}
