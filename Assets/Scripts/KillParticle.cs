using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 0.4f);
	}
}
