// UnityScript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mathf;



public class Claw : MonoBehaviour
{
    public TriggerGrip Gripper; //gets grip % from a custom script (Grip Percent game object)
    public Transform finger1;
    public Transform finger2;
    public float MaxRotation;


    void Update()
    {

        float amountToRotate = Gripper.getGrip() * (MaxRotation / 100);
        Vector3 newRotation = new Vector3(0, amountToRotate, 0);
        finger1.transform.localEulerAngles = newRotation;
        finger2.transform.localEulerAngles = newRotation;

    }

}

