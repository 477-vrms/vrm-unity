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
    public float maxGrip = 100;
    public float speed;
    public float triggerFloatL = 0;
    public float triggerFloatR = 0;
    public float triggerFloat = 0;
    public bool instant;

    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        //float triggerFloatL = 0;
        //float triggerFloatR = 0;
        //float triggerFloat = 0;
        

        foreach (var device in inputDevices)
        {
            //if (device.characteristics.ToString().Contains("Controller, Left"))
            //{
            //    device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatL);
            //}
            if (device.characteristics.ToString().Contains("Controller, Right"))
            {
                device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerFloatR);
            }
        }
        //triggerFloat = Mathf.Max(triggerFloatR, triggerFloatL);
        if(instant == true)
        {
            triggerFloat = triggerFloatR;
        }
        else
        {
            triggerFloatR = Mathf.Min(triggerFloatR, maxGrip / 100);
            if (triggerFloatR < 0.01)
            {
                triggerFloatR = 0;
            }
            if (Mathf.Abs(triggerFloatR - triggerFloat) > 0.01)
            {
                if (triggerFloat < triggerFloatR)
                {
                    triggerFloat += speed;
                }
                else if (triggerFloat > triggerFloatR)
                {
                    triggerFloat -= speed;
                }
            }
        }
        
        
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
        return Mathf.Min(GripPercent, maxGrip);
    }
    public float getMaxGrip()
    {
        return GripPercent;
    }


    public void lockGrip()
    {
        maxGrip = getGrip();
    }

    public void resetGrip()
    {
        maxGrip = 100;
    }
}

