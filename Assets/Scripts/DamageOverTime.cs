using UnityEngine;
using System.Collections;

public class DamageOverTime : MonoBehaviour {
	
	void Start(){
		Destroy(gameObject, 1f);
	}

	void OnTriggerStay(Collider collision){
		if(collision.gameObject.tag == "Enemy"){
            if (collision.gameObject.GetComponent<EnemyController>() != null)
            {
                collision.gameObject.GetComponent<EnemyController>().DecreaseLife(0.05f);
            }
            else if (collision.gameObject.GetComponent<EnemyFollowAi>() != null)
            {
                collision.gameObject.GetComponent<EnemyFollowAi>().DecreaseLife(0.05f);
            }
            else if (collision.gameObject.GetComponent<EnemyBoss>() != null)
            {
                collision.gameObject.GetComponent<EnemyBoss>().DecreaseLife(0.05f);
            }
        }
	}
}
