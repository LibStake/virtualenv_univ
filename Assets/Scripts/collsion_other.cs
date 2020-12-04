using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collsion_other : MonoBehaviour
{
    GameObject portal;
    Transform target;
    // Start is called before the first frame update

    void Awake() {
        portal = GameObject.Find("exitportal");
        target = portal.GetComponent<Portal>().target;
    }

    void Start()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject == portal)
        {
            Debug.Log("portal_on");
            transform.position = target.position;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
