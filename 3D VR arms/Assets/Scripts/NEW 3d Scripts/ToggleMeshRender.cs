using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMeshRender : MonoBehaviour
{
    // Start is called before the first frame update
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggleMeshRenderer()
    {
        rend.enabled = !rend.enabled;
    }
}
