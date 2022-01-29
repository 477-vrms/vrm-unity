using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAllButPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(10, 0);
        Physics.IgnoreLayerCollision(10, 1);
        Physics.IgnoreLayerCollision(10, 2);
        Physics.IgnoreLayerCollision(10, 3);
        Physics.IgnoreLayerCollision(10, 4);
        Physics.IgnoreLayerCollision(10, 5);
        Physics.IgnoreLayerCollision(10, 6);
        Physics.IgnoreLayerCollision(10, 7);
        //player is layer 8
        Physics.IgnoreLayerCollision(10, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
