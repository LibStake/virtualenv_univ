using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPillar : MonoBehaviour
{
    // Vector pos for teleportable position {X, Y, Z, ViewingRotation}
    public Transform dest;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.position = dest.GetComponent("ExitPos").transform.position;
            other.transform.rotation = dest.GetComponent("ExitPos").transform.rotation;
        }
    }
}
