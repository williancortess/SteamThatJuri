using UnityEngine;
using System.Collections;

public class BlockMovemente : MonoBehaviour {
	public Vector3 range;
	public Vector3 initialPosition;
	public float speed = 0.7f;
	Rigidbody _rb;


	// Use this for initialization
	void Start () {
		initialPosition = gameObject.transform.position;
		_rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_rb.MovePosition (MoveBlock());
	}

	Vector3 MoveBlock (){
		return new Vector3 (
			initialPosition.x + (range.x * Mathf.Sin (Time.time / speed)),
			initialPosition.y + (range.y * Mathf.Cos (Time.time / speed)),
			initialPosition.z
		);
	}
		

}
