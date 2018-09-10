using UnityEngine;
using System.Collections;

public class IsPlayerOnBlock : MonoBehaviour {
	Vector3 offset;
	GameObject targetPlayer;

	void Start(){
		targetPlayer = null;
	}

	void OnTriggerStay(Collider col){
		if (col.tag == "Player") {
			targetPlayer = col.gameObject;
			offset = new Vector3 (0f, targetPlayer.transform.position.y - this.transform.position.y, 0f);
		}
	}

	void OnTriggerExit(Collider col){
		targetPlayer = null;
	}

	void FixedUpdate(){
		if (targetPlayer != null) {
			targetPlayer.transform.position = this.transform.position + offset;
		}
	}
		
}
