using UnityEngine;
using System.Collections;

public class IsPlayerGrounded : MonoBehaviour {
	GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }    

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			player.GetComponent<PlayerController> ().isGrounded = true;
			player.GetComponent<PlayerController> ().speed = 8.0f;		
		}
	}

	void OnTriggerStay(Collider col){
		if (col.tag == "Player") {
			player.GetComponent<PlayerController> ().isGrounded = true;
			player.GetComponent<PlayerController> ().speed = 8.0f;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			player.GetComponent<PlayerController> ().isGrounded = false;
			player.GetComponent<PlayerController> ().speed = 7.0f;
		}
	}
}
