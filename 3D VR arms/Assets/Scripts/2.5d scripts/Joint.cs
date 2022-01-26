using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    public Joint m_child;
    //float minRotation = -90;
    //float maxRotation = 0;


    public Joint GetChild()
    {
        return m_child;
    }

    public void Rotate(float _angle)
    {
        transform.Rotate(Vector3.up * _angle);
        //trying to limit freedom
        //float angly = 
        //transform.Rotate(Vector3.up * Mathf.Clamp(_angle, minRotation, maxRotation));
    }
}
