// UnityScript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mathf;



 public class JointText : MonoBehaviour
{
    public Transform Joint1;
    //public Vector3 Axis1;
    public Transform Joint2;
    //public Vector3 Axis2;
    public Transform Joint3;
    //public Vector3 Axis3;
    public Transform Joint4;
    //public Vector3 Axis4;
    public Transform Joint5;
    //public Vector3 Axis5;
    public Transform Joint6;
    //public Vector3 Axis6;
    public Transform Joint7;
    //public Vector3 Axis7;


    void Update()
    {
        gameObject.GetComponent<TextMesh>().text = "Joint 1, Y: "+ (UnityEditor.TransformUtils.GetInspectorRotation(Joint1.transform)).y.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 2, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint2.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 3, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint3.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 4, Y: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint4.transform)).y.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 5, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint5.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 6, X: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint6.transform)).x.ToString();
        gameObject.GetComponent<TextMesh>().text += "\nJoint 7, Z: " + (UnityEditor.TransformUtils.GetInspectorRotation(Joint7.transform)).z.ToString();

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        float triggerFloatL = 0;
        float triggerFloatR = 0;
        float triggerFloat = 0;

        foreach (var device in inputDevices)
        {
            //gameObject.GetComponent<TextMesh>().text += (string.Format("\nDevice found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
            if (device.role.ToString() == "LeftHanded")
            {
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatL);
            }
            if (device.role.ToString() == "RightHanded")
            {
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatR);
            }
        }
        triggerFloat = Mathf.Max(triggerFloatL, triggerFloatR);
        if (triggerFloat < 0.01)
        {
            gameObject.GetComponent<TextMesh>().text += "\nJoint 8, C: 0%";
        }
        else
        {
            gameObject.GetComponent<TextMesh>().text += "\nJoint 8, C: " + (Mathf.Round(triggerFloat * 100f) / 100f)*100 + "%";
        }

    }
}

