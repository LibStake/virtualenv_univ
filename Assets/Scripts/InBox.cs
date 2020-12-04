using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBox : MonoBehaviour
{
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ball") {
            count++;
            Debug.Log(count + "ball in");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
