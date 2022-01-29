using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowClawToGrab : MonoBehaviour
{
    public GameObject Finger1;
    public GameObject Finger2;
    public TriggerGrip Gripper;
    public Claw claw;
    public GameObject ClawBase;

    bool m_Started;
    public LayerMask m_LayerMask;
    string state = "not grabbed";
    private Rigidbody rb;
    private GameObject oldParent;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    private Vector3 velocity = new Vector3(0, 0, 0);

    Vector3 m_DetachVelocity;
    Vector3 m_DetachAngularVelocity;

    List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldParent = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.lossyScale / 2, Quaternion.identity, m_LayerMask);
        
        if (hitColliders.Length == 2 && Gripper.getGrip() != 0 && state == "not grabbed")
        {
            state = "grabbed";
            Gripper.lockGrip();
            gameObject.transform.parent = ClawBase.transform;
            rb.isKinematic = true;
            
        }
        if (state == "grabbed" && hitColliders.Length < 2 && Gripper.maxGrip > Gripper.getMaxGrip()) //DROPPED
        {
            Gripper.resetGrip();
            rb.isKinematic = false;
            state = "not grabbed";
            gameObject.transform.parent = oldParent.transform;
            rb.velocity = velocity;

        }
        else if(state == "grabbed") //held (this part doesnt do angular momentum)
        {
            transform.position += new Vector3(0f, 0f, 0f);

            velocity = (transform.position- lastPosition) / Time.fixedDeltaTime;

            lastPosition = transform.position;
        }
        

    }

    //void Detach()
    //{
    //        rb.velocity = gameObject.XRGrabInteractable.m_DetachVelocity;
    //        rb.angularVelocity = m_DetachAngularVelocity;
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

        //}
        //void OnCollisionExit(Collision collision)
        //{
        //    list.Remove(collision.gameObject);
        //    if (collision.gameObject.layer == 6)
        //    {
        //        Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        //    }
        //    if (collision.gameObject == Finger1 && collision.gameObject == Finger2)
        //    {
        //        Debug.Log("HELLO");
        //    }

        //}
    }
