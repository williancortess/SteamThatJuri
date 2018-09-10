using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

	public Transform StartPoint;
	public Transform EndPoint;
	public EnterExitScript _enterExit;


	private Rigidbody m_rb;
	private GameObject _player;
	private bool m_movePlayer;
	private float m_timeScale = 1.0f;
	private Vector3 directionV;
	private bool _foward = true;



	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		m_rb = GetComponent<Rigidbody>();
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void FixedUpdate () {		
		if(_foward == true){
			directionV = (EndPoint.position - transform.position).normalized;
		}
		else if(_foward == false){			
			directionV = (StartPoint.position - transform.position).normalized;
		}
		m_rb.MovePosition(transform.position + directionV * Time.fixedDeltaTime);
		if(_enterExit.begin){
			_player.transform.parent = this.gameObject.transform;
		}			
		else{
			_player.transform.parent = null;
		}
	}    		

	void OnTriggerEnter(Collider col){
		if(_foward == true && col.gameObject.tag == "Trigger"){
			_foward = false;
		}
		else if(_foward == false && col.gameObject.tag == "Trigger"){
			_foward = true;
		}
	}

}
