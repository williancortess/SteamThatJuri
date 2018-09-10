using UnityEngine;
using System.Collections;

public class DetectedPlayer : MonoBehaviour
{
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
            playerDetected = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
