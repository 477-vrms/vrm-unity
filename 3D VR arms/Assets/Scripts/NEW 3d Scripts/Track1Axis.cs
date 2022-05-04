using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track1Axis : MonoBehaviour
{
    public Vector3 axis;
    public GameObject target;
    public Material zMat;
    Color tempcolor;
    float Closeness;
    // Start is called before the first frame update
    void Start()
    {
        tempcolor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Closeness = 10;
        if(axis.z == 1)
        {
            gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
            Closeness = Mathf.Abs(gameObject.transform.position.z - target.transform.position.z);
        }
        if (axis.y == 1)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
        }
        if (axis.x == 1)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, target.transform.position.y, target.transform.position.z);
            Closeness = Mathf.Abs(gameObject.transform.position.x - target.transform.position.x);
        }
        if (Closeness < 0.1) { Closeness = 0; }
        tempcolor.a = 1.8F - Closeness;
        GetComponent<MeshRenderer>().material.color = tempcolor;
    }
}
