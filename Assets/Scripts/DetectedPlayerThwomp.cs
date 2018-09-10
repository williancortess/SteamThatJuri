using UnityEngine;
using System.Collections;

public class DetectedPlayerThwomp : MonoBehaviour {

    GameObject player;
    public bool playerDetected = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("PLAYER ENCONTRADO 1");
            playerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerDetected = false;
        }
    }
}
