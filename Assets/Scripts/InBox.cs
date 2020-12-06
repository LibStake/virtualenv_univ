using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class InBox : MonoBehaviour
{
    Text bcText;
    GameObject clCanvas;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        bcText = GameObject.Find("ballcounttext").GetComponent<Text>();
        bcText.text = count.ToString();
        clCanvas = GameObject.Find("ClearCanvas");
        clCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ball") {
            count++;
            bcText.text = count.ToString();
            if (count == 3) {
                clCanvas.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
