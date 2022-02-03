using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowClawToGrab2 : MonoBehaviour
{
    public TriggerGrip Gripper;
    public Claw claw;
    public GameObject ClawBase;

    bool m_Started;
    string state = "not grabbed";
    private Rigidbody rb;
    private GameObject oldParent;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    private Vector3 velocity = new Vector3(0, 0, 0);
    private Vector3 rotation = new Vector3(0, 0, 0);
    private Vector3 lastRotation = new Vector3(0, 0, 0);
    public float rotationMultiplier = 0;
    public float velocityMultiplier = 0;

    Vector3 m_DetachVelocity;
    Vector3 m_DetachAngularVelocity;

    //int fingersTouching = 0;

    private List<string> listofnames = new List<string>();

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
        
        //Debug.Log(fingersTouching.ToString());
        if (listofnames.Count > 1 && Gripper.getGrip() != 0 && Gripper.getGrip() != 100 && state == "not grabbed")
        {
            if (claw.isGrabbed == false)
            {
                //Debug.Log(state);
                //Debug.Log("GRABBED");
                state = "grabbed";
                Gripper.lockGrip();
                gameObject.transform.parent = ClawBase.transform;
                rb.isKinematic = true;
                claw.isGrabbed = true;
            }
            

        }
        else if (state == "grabbed" && Gripper.maxGrip > Gripper.getMaxGrip()) //DROPPED
        {
            //fingersTouching = 0;
            //Debug.Log("DROPPED");
            Gripper.resetGrip();
            rb.isKinematic = false;
            state = "not grabbed";
            gameObject.transform.parent = oldParent.transform;
            rb.velocity = velocity*velocityMultiplier;
            rb.angularVelocity = rotation*rotationMultiplier;
            //Debug.Log(rotation);
            claw.isGrabbed = false;

        }
        else if (state == "grabbed") //held (this part doesnt do angular momentum)
        {
            transform.position += new Vector3(0f, 0f, 0f);

            velocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
            rotation = (transform.eulerAngles - lastRotation) / Time.fixedDeltaTime;

            lastPosition = transform.position;
            lastRotation = transform.eulerAngles;
        }


    }

    //void Detach()
    //{
    //        rb.velocity = gameObject.XRGrabInteractable.m_DetachVelocity;
    //        rb.angularVelocity = m_DetachAngularVelocity;
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 3 || collision.gameObject.layer == 8)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        if (collision.gameObject.layer == 7)
        {
            if(!listofnames.Contains(collision.gameObject.name))
            {
                listofnames.Add(collision.gameObject.name);
                //Debug.Log("added");
                //Debug.Log(listofnames.Count);
            }
            //if(fingersTouching > 1 && state == "not grabbed")
            //{
            //    state == "grabbed";
            //}
        }
    }

    //}
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 3)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        if (collision.gameObject.layer == 7)
        {
            //if(state == "grabbed")
            //{
                if (listofnames.Contains(collision.gameObject.name))
                {
                    listofnames.Remove(collision.gameObject.name);
                    //Debug.Log("removed");
                    //Debug.Log(listofnames.Count);
                }
            //}
        }

    }
}
