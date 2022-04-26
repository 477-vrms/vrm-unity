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
                if (Item1.layer == 5) { SetLayerRecursively(Item1, 3); }
                else { SetLayerRecursively(Item1, 5); }
                if (Item2.layer == 5) { SetLayerRecursively(Item2, 3); }
                else { SetLayerRecursively(Item2, 5); }
                //Item1.SetActive(!Item1.activeSelf);
                //Item2.SetActive(!Item1.activeSelf);
                isHeld = true;
            }
        }
        else
        {
            isHeld = false;
        }


    }
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}