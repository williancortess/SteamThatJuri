using UnityEngine;
using System.Collections;

public class EnterExitScript : MonoBehaviour {

	public bool begin = false;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			begin = true;
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Player"){
			begin = false;
		}
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.tag == "Player"){
			begin = true;
		}
	}



}
