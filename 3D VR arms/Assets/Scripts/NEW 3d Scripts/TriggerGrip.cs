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
            GripPercent = 0;
        }
        else
        {
            GripPercent = (Mathf.Round(triggerFloat * 100f) / 100f) * 100;
        }
        

    }

    public float getGrip()
    {
        return GripPercent;
    }
}

