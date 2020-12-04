using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPillar : MonoBehaviour
{
    // Vector pos for teleportable position {X, Y, Z, ViewingRotation}
    private Vector4 depart = new Vector4(0f, 0f, 0f);
    private Vector4 dest = new Vector4(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDeparture(Vector4 pos)
    {
        depart = pos;
    }

    public void setDestination(Vector4 pos)
    {
        dest = pos;
    }
}
