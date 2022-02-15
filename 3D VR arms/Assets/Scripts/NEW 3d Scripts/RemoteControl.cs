using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControl : MonoBehaviour
{
    public GameObject whatToCopy;
    private Vector3 offset;
    private Vector3 offsetR;
    private GameObject CopierParent;
    //bool copierBeingGrabbed;
    public bool RotationOffset;

    public Material normal;
    public Material Controlled;
    public float multiplier;
    private Vector3 OldCopierPosition;
    // Start is called before the first frame update

    //reset stuff
    public Vector3 StartingPosition;
    public Quaternion StartingRotation;


    void Start()
    {
        offset = whatToCopy.transform.position - transform.position;
        offsetR = whatToCopy.transform.rotation.eulerAngles - transform.rotation.eulerAngles ;
        OldCopierPosition = whatToCopy.transform.position;
        StartingPosition = transform.position;
        StartingRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.Two)) //B button
        {
            gameObject.GetComponent<MeshRenderer>().material = Controlled;
            //copierBeingGrabbed = true;
            //transform.position = (whatToCopy.transform.position - offset);
            transform.position += (whatToCopy.transform.position- OldCopierPosition)*multiplier;
            OldCopierPosition = whatToCopy.transform.position;
            if (RotationOffset)
            {
                transform.rotation = Quaternion.Euler(whatToCopy.transform.rotation.eulerAngles - offsetR);
            }
            else
            {
                transform.rotation = Quaternion.Euler(whatToCopy.transform.rotation.eulerAngles);
            }
            
        }
        else if(OVRInput.Get(OVRInput.Button.Two) == false)
        {
            //copierBeingGrabbed = false;
            gameObject.GetComponent<MeshRenderer>().material = normal;
            offset = whatToCopy.transform.position - transform.position;
            offsetR = whatToCopy.transform.rotation.eulerAngles - transform.rotation.eulerAngles;
            OldCopierPosition = whatToCopy.transform.position;
        }
        
    }
    public void changeMultiplier(float newValue)
    {
        multiplier = newValue;
    }
    public void changeRotOffset()
    {
        RotationOffset = !RotationOffset;
    }
    public void resetTarget()
    {
        if (GetComponent<Animator>().enabled)
        {
            //do nothing during animation
        }
        else
        {
            transform.position = StartingPosition;
            transform.rotation = StartingRotation;
        }
    }
}