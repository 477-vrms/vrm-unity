// UnityScript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JointText : MonoBehaviour
{
    public Transform Joint1;
    public Transform Joint2;
    public Transform Joint3;
    public Transform Joint4;
    public Transform Joint5;
    public Transform Joint6;
    public Transform Joint7;
    public TriggerGrip Gripper;
    public TextMeshPro textDisplay;


    void Update() //SPAGHETTI CODE
    {
        //Way to get inspector rotation (doesnt work on builds)
        //UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)

        textDisplay.text = "Joint 1, Y: " + Account(Joint1.transform.localEulerAngles, "y").ToString("F2");
        textDisplay.text += "\nJoint 2, X: " + Account(Joint2.transform.localEulerAngles, "x").ToString("F2");
        textDisplay.text += "\nJoint 3, X: " + Account(Joint3.transform.localEulerAngles, "x").ToString("F2");
        //textDisplay.text += "\nJoint 4 (not used)";// + Account(Joint4.transform.localEulerAngles, "y").ToString("F2");
        textDisplay.text += "\nJoint 4, Y: " + Account(Joint5.transform.localEulerAngles, "y").ToString("F2");
        textDisplay.text += "\nJoint 5, Y: " + Account(Joint6.transform.localEulerAngles, "y").ToString("F2");
        textDisplay.text += "\nJoint 6, Z: " + Account(Joint7.transform.localEulerAngles, "z").ToString("F2");
        textDisplay.text += "\nJoint 7, Claw: " + Gripper.getGrip() + "%";
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


