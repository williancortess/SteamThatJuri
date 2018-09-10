using UnityEngine;
using System.Collections;

public class HitScript : MonoBehaviour {

	public bool canHitEnemy;
	public GameObject enemy;


	void FixedUpdate(){
		if(enemy == null){
			canHitEnemy = false;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Enemy"){
			enemy = col.gameObject;
			canHitEnemy = true;
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Enemy"){			
			enemy = null;
		}
	}
}
