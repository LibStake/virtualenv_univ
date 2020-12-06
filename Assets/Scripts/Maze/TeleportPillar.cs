using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPillar : MonoBehaviour
{
    // Vector pos for teleportable position {X, Y, Z, ViewingRotation}
    public Transform dest;
    public bool disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (disabled)
        {
            transform.parent.GetChild(3).GetComponent<Light>().enabled = false;
            transform.parent.GetChild(4).GetComponent<Light>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnable(bool enable)
    {
        disabled = !enable;
        if (!disabled)
        {
            transform.parent.GetChild(3).GetComponent<Light>().enabled = true;
            transform.parent.GetChild(4).GetComponent<Light>().enabled = false;
        }
        else
        {
            transform.parent.GetChild(3).GetComponent<Light>().enabled = false;
            transform.parent.GetChild(4).GetComponent<Light>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disabled) return;
        if (dest == null) return;
        
        if (other.gameObject.transform.CompareTag("Player"))
        {
            Debug.Log("Teleport TO " + dest.position.x + ", " + dest.position.y + ", " + dest.position.z + "R : " + dest.rotation.y);
            other.transform.SetPositionAndRotation(dest.position, dest.rotation);
        }
    }
}
