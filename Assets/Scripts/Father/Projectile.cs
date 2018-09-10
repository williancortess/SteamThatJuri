using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour{

	public int m_damage;
	public int m_lifeTime;


	protected virtual void checkComands(){
	}

	protected virtual void collidesAction(Collision collision){
	}
}
