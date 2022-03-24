using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject joint;
    public bool isMax;
    public bool isIKArm;
    public bool isElbow;
    void Start()
    {
        if(isIKArm == true)
        {
            if(isElbow == true)
            {
                if (isMax == true)
                {
                    UpdateMaxE();
                }
                else
                {
                    UpdateMinE();
                }
            }
            else
            {
                if (isMax == true)
                {
                    UpdateMaxO();
                }
                else
                {
                    UpdateMinO();
                }
            }
        }
        else
        {
            if (isMax == true)
            {
                UpdateMax();
            }
            else
            {
                UpdateMin();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMax()
    {
        GetComponent<Slider>().value = joint.GetComponent<IKAxis>().Maximum;
    }
    public void UpdateMin()
    {
        GetComponent<Slider>().value = joint.GetComponent<IKAxis>().Minimum;
    }
    public void UpdateMaxO()
    {
        GetComponent<Slider>().value = joint.GetComponent<IKArm>().MaximumO;
    }
    public void UpdateMinO()
    {
        GetComponent<Slider>().value = joint.GetComponent<IKArm>().MinimumO;
    }
    public void UpdateMaxE()
    {
        GetComponent<Slider>().value = joint.GetComponent<IKArm>().MaximumE;
    }
    public void UpdateMinE()
    {
        GetComponent<Slider>().value = joint.GetComponent<IKArm>().MinimumE;
    }
    public void UpdateSliderValue()
    {
        Start();
    }
}
