using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	public GameObject model;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = model.GetComponent<Animator>();
	}

	public void ActivateIdle(){
		_animator.Play("Idle");
	}

	public void ActivateJump(){
		_animator.Play("jump");
	}

	public void ActivateRun(){		
		_animator.Play("run");
	}

	public void ActivatePunch(){
		_animator.Play("punch1");
	}

	public void ActivateDamage(){
		_animator.Play("damage");
	}

	public void TurnRight(){
		model.transform.rotation = Quaternion.LookRotation(Vector3.right);
	}

	public void TurnLeft(){
		model.transform.rotation = Quaternion.LookRotation(Vector3.left);
	}
}
