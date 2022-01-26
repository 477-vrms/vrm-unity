using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    //This class is for objects you want to use lookat for are a child of another lookat.
    public Transform target;
    public Quaternion parent;
    public Vector3 Axis;
    public Vector3 offsets;
    public float Speed = 1;


    void Update()
    {

        //Vector3 newRotation = new Vector3(target.transform.localRotation.eulerAngles.x, target.transform.localRotation.eulerAngles.y, target.transform.localRotation.eulerAngles.z);
        Vector3 newRotation = UnityEditor.TransformUtils.GetInspectorRotation(target.transform);
        if (Axis.x == 1)
        {
            //newRotation = new Vector3(gameObject.transform.localRotation.eulerAngles.x, 0f, 0f);
            newRotation = new Vector3(UnityEditor.TransformUtils.GetInspectorRotation(target.transform).x, offsets.y, offsets.z);

        }
        else if (Axis.y == 1)
        {
            //newRotation = new Vector3(0f, gameObject.transform.localRotation.eulerAngles.y, 0f);
            newRotation = new Vector3(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x, UnityEditor.TransformUtils.GetInspectorRotation(target.transform).y, UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).z);
            //Debug.Log("y: " + UnityEditor.TransformUtils.GetInspectorRotation(target.transform).y);
        }
        else if (Axis.z == 1)
        {
            //newRotation = new Vector3(0f, 0f, gameObject.transform.localRotation.eulerAngles.z);
            newRotation = new Vector3(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x, UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y, UnityEditor.TransformUtils.GetInspectorRotation(target.transform).z);
        }
        
        gameObject.transform.localEulerAngles = newRotation;




    }

}
