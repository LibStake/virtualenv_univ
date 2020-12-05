using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public GameObject startCanvas;
    // Start is called before the first frame update

   public void onStart()
    {
        startCanvas.SetActive(false);
    }
}
