// UnityScript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    void Update()
    {
        gameObject.GetComponent<TextMesh>().text = "Joint 1, Y: "+ (UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)).y.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 2, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint2.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 3, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint3.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 4, Y: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint4.transform)).y.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 5, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint5.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 6, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint6.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 7, Z: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint7.transform)).z.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 8, C: " + Gripper.getGrip() + "%";
    }
}

