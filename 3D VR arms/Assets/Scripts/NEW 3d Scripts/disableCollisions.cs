using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 0);
        Physics.IgnoreLayerCollision(8, 1);
        Physics.IgnoreLayerCollision(8, 2);
        Physics.IgnoreLayerCollision(8, 3);
        Physics.IgnoreLayerCollision(8, 4);
        Physics.IgnoreLayerCollision(8, 5);
        Physics.IgnoreLayerCollision(8, 6);
        Physics.IgnoreLayerCollision(8, 7);
        Physics.IgnoreLayerCollision(8, 9);
        Physics.IgnoreLayerCollision(8, 9);
        //10 is target
        //11 is ground
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
