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

        Vector3 newRotation = new Vector3(0,0,0);// = UnityEditor.TransformUtils.GetInspectorRotation(target.transform);
        if (Axis.x == 1)
        {
            //newRotation = new Vector3(UnityEditor.TransformUtils.GetInspectorRotation(target.transform).x, offsets.y, offsets.z);
            newRotation = new Vector3(Account(target.transform.localEulerAngles, "x"), 0, 0);
        }
        else if (Axis.y == 1)
        {
            //newRotation = new Vector3(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x, UnityEditor.TransformUtils.GetInspectorRotation(target.transform).y, UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).z);
            newRotation = new Vector3(0, Account(target.transform.localEulerAngles, "y"), 0);
        }
        else if (Axis.z == 1)
        {
            //newRotation = new Vector3(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x, UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y, UnityEditor.TransformUtils.GetInspectorRotation(target.transform).z);
            newRotation = new Vector3(0, 0, Account(target.transform.localEulerAngles, "z"));        
        }
        
        gameObject.transform.localEulerAngles = newRotation;




    }
    public float Account(Vector3 angle, string axis)
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

        return newAngle;
    }

}
