using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAnObject : MonoBehaviour
{
    public GameObject Item1;
    public GameObject Item2;
    private bool isHeld = false;
    void Start()
    {
        
    }

    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.Three))
        {
            if (isHeld == false)
            {
                Item1.SetActive(!Item1.activeSelf);
                Item2.SetActive(!Item1.activeSelf);
                isHeld = true;
            }
        }
        else
        {
            isHeld = false;
        }


    }
}