using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyBoss : MonoBehaviour {

	public enum EnemyState { Idle, Chasing, Atack01, Atack02, Fall}

	public GameObject model;
	private Animator _animator;

	private float _speed = 5.0f;
	private float _maxVelocityChange = 10.0f;
	private float _walkingDirection = 1.0f;
	private float _enemyLife = 25;
	private float _maxLife = 25;
	private Rigidbody _rb;
	private ForceMode _currentForceMode;
	private EnemyState _currState;
	private bool _isOnCorroutine = false;
	public Transform pivotPosition1;
	public Transform pivotPosition2;
	public Rigidbody bullet;
	public Transform player;

	// Use this for initialization
	void Start () {
		_animator = model.GetComponent<Animator>();
		_animator.Play("Walk");
		_rb = GetComponent<Rigidbody>();
		_rb.freezeRotation = true;	
		_currState = EnemyState.Chasing;
		_currentForceMode = ForceMode.VelocityChange;
	}

	// Update is called once per frame
	void Update () {
		ChangeDirection();
		UpdateGameState();
	}

	void EnterGameState(EnemyState newState)
	{	

		_currState = newState;
		switch (_currState)
		{
		case EnemyState.Idle:
			{			
				_rb.velocity = Vector3.zero;
			}
			break;

		case EnemyState.Chasing:
			{	
				_animator.Play("Walk");
			}
			break;

		case EnemyState.Atack01:
			{
				_rb.velocity = Vector3.zero;
				_animator.Play("Atack01");
			}
			break;
		case EnemyState.Atack02:
			{
				_rb.velocity = Vector3.zero;
				_animator.Play("Atack02");
			}
			break;	
		case EnemyState.Fall:
			{

			}
			break;	
		}
	}

	//Actions when the player is in the GameState;
	void UpdateGameState()
	{
		switch (_currState)
		{
		case EnemyState.Idle:
			{			

			}
			break;

		case EnemyState.Chasing:
			{	
				Move();
				float x = player.transform.position.x - gameObject.transform.position.x;
				//Debug.Log(x);
				if(x > -3 && _walkingDirection == -1){
					EnterGameState(EnemyState.Atack01);
				}
				else if(x < 3 && _walkingDirection == 1){
					EnterGameState(EnemyState.Atack01);
				}
				else if(x < -15 && _walkingDirection == -1){
					EnterGameState(EnemyState.Atack02);
				}
				else if(x > 15 && _walkingDirection == 1){
					EnterGameState(EnemyState.Atack02);
				}
			}
			break;

		case EnemyState.Atack01:
			{				
				if(!_isOnCorroutine){
					StartCoroutine(WaitNextAction(1.15f, EnemyState.Chasing));
					_isOnCorroutine = true;
				}
			}
			break;
		case EnemyState.Atack02:
			{
				if(!_isOnCorroutine){
					Vector3 direction = (player.transform.position - transform.position).normalized;
					Rigidbody clone;
					if(_walkingDirection < 0){
						clone = (Rigidbody)Instantiate(bullet, pivotPosition1.position, transform.rotation);
						clone.AddForce(direction * 15.0f, ForceMode.Impulse);
					}
					else if(_walkingDirection > 0){
						clone = (Rigidbody)Instantiate(bullet, pivotPosition2.position, transform.rotation);
						clone.AddForce(direction * 15.0f, ForceMode.Impulse);
					}
					StartCoroutine(WaitNextAction(1.15f, EnemyState.Chasing));
					_isOnCorroutine = true;
				}
			}
			break;	
		case EnemyState.Fall:
			{

			}
			break;	
		}
	}

	void ChangeDirection(){
		float x = player.transform.position.x - gameObject.transform.position.x;
		if(x > 0){
			_walkingDirection = 1;
			model.transform.rotation = Quaternion.LookRotation(Vector3.right);
		}
		if(x < 0){
			_walkingDirection = -1;
			model.transform.rotation = Quaternion.LookRotation(Vector3.left);
		}
	}

	void Move(){

		Vector3 targetVelocity = new Vector3(_walkingDirection, 0, 0);
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= _speed;

		Vector3 velocity = _rb.velocity;	
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -_maxVelocityChange, _maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -_maxVelocityChange, _maxVelocityChange);
		velocityChange.y = 0;

		_rb.AddForce(velocityChange, _currentForceMode);

	}

	IEnumerator WaitNextAction(float time, EnemyState state){
		yield return new WaitForSeconds(time);
		_isOnCorroutine = false;
		EnterGameState(state);
	}

	public void DecreaseLife(float damage){
		_enemyLife -= damage;
		Debug.Log(_enemyLife);
		if(_enemyLife <= 0){			
			Debug.Log("KilledBoss");
            SceneManager.LoadScene("Menu");
            Destroy(gameObject);
		}
	}

}
