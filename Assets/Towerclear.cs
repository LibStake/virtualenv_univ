using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towerclear : MonoBehaviour
{
    public GameObject capsule;

    void Awake() {
        capsule.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Controller").GetComponent<GameMaster>().setQuizClear();
            capsule.SetActive(true);
        }
    }
}
