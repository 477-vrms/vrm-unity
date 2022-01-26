// UnityScript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mathf;



public class TriggerGrip : MonoBehaviour
{
    [ReadOnly]
    public float GripPercent;


    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        float triggerFloatL = 0;
        float triggerFloatR = 0;
        float triggerFloat = 0;

        foreach (var device in inputDevices)
        {
            if (device.characteristics.ToString().Contains("Controller, Left"))
            {
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatL);
            }
            if (device.characteristics.ToString().Contains("Controller, Right"))
            {
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatR);
            }
        }
        triggerFloat = Mathf.Max(triggerFloatL, triggerFloatR);
        if (triggerFloat < 0.01)
        {
            GripPercent = 0;
        }
        else
        {
            //GripPercent = (Mathf.Round(triggerFloat * 100f) / 100f) * 100;
            GripPercent = (float)System.Math.Round(triggerFloat*100, 1);
        }
        

    }

    public float getGrip()
    {
        return GripPercent;
    }
}

