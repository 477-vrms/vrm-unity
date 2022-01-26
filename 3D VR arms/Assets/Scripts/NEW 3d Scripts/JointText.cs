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
    public TriggerGrip Gripper; //gets grip % from a custom script (Grip Percent game object)
    public TextMeshPro textDisplay;


    void Update()
    {
        
        textDisplay.text = "Joint 1, Y: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)).y.ToString("F2");
        textDisplay.text += "\nJoint 2, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint2.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 3, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint3.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 4, Y: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint4.transform)).y.ToString("F2");
        textDisplay.text += "\nJoint 5, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint5.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 6, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint6.transform)).x.ToString("F2");
        textDisplay.text += "\nJoint 7, Z: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint7.transform)).z.ToString("F2");
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
}

