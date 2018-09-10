using UnityEngine;
using System.Collections;

public class DetectedThwompDown : MonoBehaviour {

    GameObject thwomp;
    public bool thwompDetected = false;

    void Awake()
    {
        thwomp = GameObject.FindGameObjectWithTag("Thwomp");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == thwomp)
        {
            thwompDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == thwomp)
        {
            thwompDetected = false;
        }
    }
}
