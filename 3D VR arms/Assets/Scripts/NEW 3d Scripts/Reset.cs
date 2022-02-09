using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public string ResetButton;
    // Start is called before the first frame update
    Vector3 StartingPosition;
    Quaternion StartingRotation;
    private bool isHeld = false;
    private Rigidbody rb;
    void Start()
    {
        StartingPosition = transform.position;
        StartingRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.Four))
        {
            if (isHeld == false)
            {
                transform.position = StartingPosition;
                transform.rotation = StartingRotation;
                rb.velocity = new Vector3(0,0,0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                isHeld = true;
            }
        }
        else
        {
            isHeld = false;
        }
        
        
    }
}
