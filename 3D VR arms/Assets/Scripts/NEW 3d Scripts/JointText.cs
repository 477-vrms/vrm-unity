// UnityScript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JointText : MonoBehaviour
{
    public Transform Joint1;
    public float Joint1Offset;
    public Transform Joint2;
    public float Joint2Offset;
    public Transform Joint3;
    public float Joint3Offset;
    public Transform Joint4;
    public float Joint4Offset;
    public Transform Joint5;
    public float Joint5Offset;
    public Transform Joint6;
    public float Joint6Offset;
    public Transform Joint7;
    public float Joint7Offset;
    public TriggerGrip Gripper; //gets grip % from a custom script (Grip Percent game object)
    public TextMeshPro textDisplay;


    void Update() //SPAGHETTI CODE, should fix these offsets things
    {

        textDisplay.text = "Joint 1, Y: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)).y.ToString("F2");
        textDisplay.text += "\nJoint 1, Y: " + (Clamp0360(Joint1.transform.localEulerAngles.y)+ Joint1Offset).ToString("F2");
        textDisplay.text += "\nJoint 2, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint2.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 2, Y: " + (Clamp0360(Joint2.transform.localEulerAngles.x) + Joint2Offset).ToString("F2");
        textDisplay.text += "\nJoint 3, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint3.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 3, Y: " + (Clamp0360(Joint3.transform.localEulerAngles.x) + Joint3Offset).ToString("F2");
        textDisplay.text += "\nJoint 4, Y: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint4.transform)).y.ToString("F2");
        textDisplay.text += "\nJoint 4, Y: " + (Clamp0360(Joint4.transform.localEulerAngles.y) + Joint4Offset).ToString("F2");
        textDisplay.text += "\nJoint 5, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint5.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 5, Y: " + (Clamp0360(Joint5.transform.localEulerAngles.x) + Joint5Offset).ToString("F2");
        textDisplay.text += "\nJoint 6, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint6.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 6, Y: " + (Clamp0360(Joint6.transform.localEulerAngles.x) + Joint6Offset).ToString("F2");
        textDisplay.text += "\nJoint 7, Z: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint7.transform)).z.ToString("F2");
        textDisplay.text += "\nJoint 7, Y: " + (Clamp0360(Joint7.transform.localEulerAngles.z) + Joint7Offset).ToString("F2");
        textDisplay.text += "\nJoint 8, C: " + Gripper.getGrip() + "%";
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Joint 1, Y: "+ (UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)).y.ToString();
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += "\nJoint 2, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint2.transform)).x.ToString();
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += "\nJoint 3, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint3.transform)).x.ToString();
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += "\nJoint 4, Y: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint4.transform)).y.ToString();
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += "\nJoint 5, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint5.transform)).x.ToString();
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += "\nJoint 6, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint6.transform)).x.ToString();
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += "\nJoint 7, Z: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint7.transform)).z.ToString();
        //gameObject.GetComponent<TMPro.TextMeshProUGUI>().text += "\nJoint 8, C: " + Gripper.getGrip() + "%";
    }
    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }
        return result;
    }
}


