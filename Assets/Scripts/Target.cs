using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    GameObject _goDetectedPlayer;
    DetectedPlayer detectedPlayer;

    void Awake()
    {
        _goDetectedPlayer = GameObject.FindGameObjectWithTag("DetectedPlayer");
        detectedPlayer = _goDetectedPlayer.GetComponent<DetectedPlayer>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (detectedPlayer.playerDetected)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
