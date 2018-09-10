using UnityEngine;
using System.Collections;

public class DealDemage : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			col.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
		}
	}
}
