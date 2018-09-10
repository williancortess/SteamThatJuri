using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public enum PlayerState {Idle ,Running, Jumping, Hit }

	public float speed = 15.0f;
	public float tapSpeed = 10f;
	public float jumpForce = 750.0f;
	public bool isGrounded;

	public bool isOnBall = false;
	public bool isOnWire = false;

	public GameObject parachute;
	public GameObject steam;
	public Magnetism magnetism;
	public WireScript wire;
	public PlayerState playerState;
    public bool WaitForShit = true;
	public float direction = 0;
	public float lastDirection = 0;
	public GameObject model;
	public GameObject SmokeBall;


	//Audio
	private AudioSource _audio;
	public AudioClip jump;
    public AudioClip atackSteam;
    private bool _waitForPunch = false;

	private Rigidbody _rb;
	private PlayerAnimator _playerAnimator;
	private bool _isButtonJumping = false;
	private bool _startJumpCoroutine = false;
	private bool _isParachuteOpen = false;

	void Start () {
		SmokeBall.SetActive(false);
		parachute.GetComponent<MeshRenderer>().enabled = false;
		parachute.GetComponent<BoxCollider>().enabled = false;
		_rb = GetComponent<Rigidbody>();
		_audio = GetComponent<AudioSource>();
		playerState = PlayerState.Idle;
		isGrounded = true;
		_playerAnimator = GetComponent<PlayerAnimator>();
	}

	void Update () {   
		if(!isOnBall && !isOnWire){	
			UpdateGameState();
			model.SetActive(true);
			SmokeBall.SetActive(false);

			if(_isButtonJumping && _rb.velocity.y < 0){
				_isParachuteOpen = true;	
				//_rb.drag = 5;
				//parachute.GetComponent<MeshRenderer>().enabled = true;
				//parachute.GetComponent<BoxCollider>().enabled = true;
			}
		}
		else{
			model.SetActive(false);
		}
	}



	void EnterGameState(PlayerState newState)
	{	

		playerState = newState;
		switch (playerState)
		{
		case PlayerState.Idle:
			{			
				_playerAnimator.ActivateIdle();	
				//Debug.Log("Enter Idle");
			}
			break;

		case PlayerState.Running:
			{	
				_playerAnimator.ActivateRun();
				//Debug.Log("Enter Running");
			}
			break;

		case PlayerState.Jumping:
			{
				_playerAnimator.ActivateJump();
				//Debug.Log("Enter Junping");
			}
			break;
		case PlayerState.Hit:
			{
				_playerAnimator.ActivatePunch();
				//Debug.Log("Enter Clinbing");
			}
			break;	
		}
	}

	//Actions when the player is in the GameState;
	void UpdateGameState()
	{
		switch (playerState)
		{
		case PlayerState.Idle:
			{

			}
			break;

		case PlayerState.Running:
			{	
				Move();
				if(isGrounded && direction == 0 && !_isButtonJumping && !_waitForPunch){
					EnterGameState(PlayerState.Idle);
				}
			}
			break;

		case PlayerState.Jumping:
			{
				Jump();
				Move();
				if(isGrounded && !_isButtonJumping && !_waitForPunch){
					EnterGameState(PlayerState.Running);
				}
			}
			break;
		case PlayerState.Hit:
			{						
				Move();
				if(!_waitForPunch){
					EnterGameState(PlayerState.Running);
				}
			}
			break;	
		}
	}

	void Jump()
	{
		if (isGrounded &&  !_startJumpCoroutine)
		{
			StartCoroutine(JumpCoroutine());
			_audio.clip = jump;
			_audio.Play();
			_rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
		}
	}

	public void MoveButton(float x){
		direction = x;
		if(direction > 0){
			lastDirection = 1;
			_playerAnimator.TurnRight();
		}
		else if(direction < 0){
			lastDirection = -1;
			_playerAnimator.TurnLeft();
		}
		if(isGrounded)
			EnterGameState(PlayerState.Running);
	}

	public void JumpButton(){
		_isButtonJumping = true;
		if (isGrounded && !isOnBall && !isOnWire){
			_startJumpCoroutine = false;
			EnterGameState(PlayerState.Jumping);
		}
	}

	public void JumpButtonUp(){				
		_isButtonJumping = false;
		_rb.drag = 0;
		//parachute.GetComponent<MeshRenderer>().enabled = false;
		//parachute.GetComponent<BoxCollider>().enabled = false;
	}

	public void HitButton(){				
		_waitForPunch = true;
        _audio.clip = atackSteam;
        _audio.Play();
        EnterGameState(PlayerState.Hit);
		if(lastDirection > 0){
			GameObject prefab = (GameObject)Instantiate(steam, transform.position, transform.rotation);
			prefab.transform.rotation = Quaternion.LookRotation(Vector3.right);
		}
		else if(lastDirection < 0){
			GameObject prefab = (GameObject)Instantiate(steam, transform.position, transform.rotation);
			prefab.transform.rotation = Quaternion.LookRotation(Vector3.left);
		}
		StartCoroutine(WaitForPunch());

	}

	IEnumerator WaitForPunch(){
		yield return new WaitForSeconds(1f);
		_waitForPunch = false;
	}

	public void EndMagnet(){
		if(magnetism != null){
			magnetism.EndMagnet();
			magnetism = null;
		}
	}

	public void BeginEndWire(){
		if(wire != null && !isOnWire){
			wire.ActivateWire();
		}
		else if(wire != null && isOnWire){
			wire.DesactivateWire();
		}
	}

	void Move(){		
		Vector3 targetVelocity = new Vector3(direction, 0, 0);
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;

		Vector3 velocity = _rb.velocity;	
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -5, 5);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -5, 5);
		velocityChange.y = 0;

		_rb.AddRelativeForce(velocityChange, ForceMode.VelocityChange);	
	}



	IEnumerator JumpCoroutine(){
		yield return new WaitForEndOfFrame();
		_startJumpCoroutine = true;
	}

    public IEnumerator WaitForS()
    {
        yield return new WaitForSeconds(1);
        WaitForShit = true;
    }

	void OnCollisionEnter(Collision collision){		
		if(collision.gameObject.tag == "Enemy" && isOnBall){
            if (collision.gameObject.GetComponent<EnemyController>() != null)
            {
                collision.gameObject.GetComponent<EnemyController>().DecreaseLife(5);
            }
            else if (collision.gameObject.GetComponent<EnemyFollowAi>() != null)
            {
                collision.gameObject.GetComponent<EnemyFollowAi>().DecreaseLife(5);
            }
            else if (collision.gameObject.GetComponent<EnemyBoss>() != null)
            {
                collision.gameObject.GetComponent<EnemyBoss>().DecreaseLife(5);
            }
        }
		isOnBall = false;
		_rb.useGravity = true;
		_rb.GetComponent<Rigidbody>().drag = 1;			
		if(isGrounded){
			_isParachuteOpen = false;
			_rb.drag = 0;
			//parachute.GetComponent<MeshRenderer>().enabled = false;
			//parachute.GetComponent<BoxCollider>().enabled = false;
		}
	}

}
