using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject joint;
    public bool isMax;
    void Start()
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
    public void UpdateSliderValue()
    {
        Start();
    }
}
