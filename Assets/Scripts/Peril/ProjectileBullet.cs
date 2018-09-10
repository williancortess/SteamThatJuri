using UnityEngine;
using System.Collections;

public class ProjectileBullet : Projectile {

	// Use this for initialization
	void Start () {		
		m_damage = 1;
		m_lifeTime = 2;
		Destroy(gameObject, m_lifeTime);
	}

	// Update is called once per frame
	void FixedUpdate () {
		checkComands();
	}

	void OnCollisionEnter(Collision collision) {
		collidesAction(collision);
	}

	protected override void checkComands(){
	}

	protected override void collidesAction(Collision collision){
		/*if(collision.gameObject.tag == "Enemy"){
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
		else */
        
        if(collision.gameObject.tag == "Player"){
            PlayerHealth pH = collision.gameObject.GetComponent<PlayerHealth>();
            pH.TakeDamage(10);

            Destroy(gameObject);
		}else {
            Destroy(gameObject);
        }
	}
}
