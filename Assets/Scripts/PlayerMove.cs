using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update

    float speed = 10;


    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            Rotate(h, v);
            Walk();
        }

    }

    void Walk()
    {
        transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
    }

    void Rotate(float h, float v)
    {
        Vector3 dir = new Vector3(h, 0, v).normalized;
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
    }
}
