using UnityEngine;
using System.Collections;

public class WireScript : MonoBehaviour {
	public Transform[] target;
	public float speed;
	public EnterExitScript enter;
	public EnterExitScript exit;
	 
	private int _current = 0;
	private bool _active = false;
	private int _check = 1;
	private Transform _player;


	void Start(){
		_player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {		
		if(_active){
			if(_check == 1){					
				MoveFoward();
			}
			else if(_check == 2){
				MoveBackward();
			}
		}
		if((enter.begin == true || exit.begin == true)){
			_player.GetComponent<PlayerController>().wire = this.GetComponent<WireScript>();
		}
	}

	public void ActivateWire(){
		if((enter.begin == true || exit.begin == true)){
			_active = true;
			_player.GetComponent<PlayerController>().isOnWire = true;
			_player.GetComponent<PlayerController>().isOnBall = false;
			_player.GetComponent<Rigidbody>().useGravity = false;
			_player.GetComponent<Rigidbody>().velocity = Vector3.zero;
			if(enter.begin == true){
				_current = 0;
				_check = 1;
				_player.transform.position = target[_current].position;
			}
			else if(exit.begin == true){				
				_current = target.Length - 1;
				_check = 2;
				_player.transform.position = target[_current].position;
			}
		}
	}

	public void DesactivateWire(){
		_active = false;
		_player.GetComponent<Rigidbody>().useGravity = true;
		_player.GetComponent<PlayerController>().isOnWire = false;
	}

	void MoveFoward(){
		if (_player.position != target[_current].position)
		{
			Vector3 pos = Vector3.MoveTowards(_player.position, target[_current].position, speed * Time.deltaTime);
			_player.GetComponent<Rigidbody>().MovePosition(pos);
		}
		else{			
			if(_current == target.Length - 1){
				_active = false;	
				_player.GetComponent<PlayerController>().isOnWire = false;
				//_current = 0;
				_player.GetComponent<Rigidbody>().useGravity = true;
			}
			else{
				_current = (_current + 1) % target.Length;
			}
		}
	}

	void MoveBackward(){
		if (_player.position != target[_current].position)
		{			
			Vector3 pos = Vector3.MoveTowards(_player.position, target[_current].position, speed * Time.deltaTime);
			_player.GetComponent<Rigidbody>().MovePosition(pos);
		}
		else{					
			if(_current == 0){
				
				_active = false;	
				_player.GetComponent<PlayerController>().isOnWire = false;
				_current = target.Length;
				_player.GetComponent<Rigidbody>().useGravity = true;
			}
			else{
				_current = _current - 1;
			}
		}
	}


}