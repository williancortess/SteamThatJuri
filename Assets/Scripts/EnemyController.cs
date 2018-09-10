using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public enum EnemyState { Patrolling, Chasing, Jumping }

    public GameObject model;
    private Animator _animator;

    private float m_speed = 2.0f;
	private float m_jumpForce = 700.0f;
	private float maxVelocityChange = 10.0f;
	private float walkingDirection = 1.0f;
	private float m_enemyLife = 3;
	private float m_maxLife = 3;
	private Vector2 walkAmount;
	private Rigidbody _rb;
	private ForceMode m_currentForceMode;
	private EnemyState m_currState;
	public Transform m_player;

    private LevelManager playerScore;
    public int scoreToDestroy = 10;

    public Image currentLifeBar;


	void Start () {
        _animator = model.GetComponent<Animator>();
        _animator.Play("walk");
        _rb = GetComponent<Rigidbody>();
		_rb.freezeRotation = true;	
		m_currState = EnemyState.Patrolling;
		m_currentForceMode = ForceMode.VelocityChange;

        playerScore = FindObjectOfType<LevelManager>();
    }

	// Update is called once per frame
	void Update () {

		float ratio = m_enemyLife/ m_maxLife;
		currentLifeBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

		ChangeDirection();
		CheckGround();
		Move(walkingDirection);
	}

	void Move(float direction){

		Vector3 targetVelocity = new Vector3(direction, 0, 0);
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= m_speed;

		Vector3 velocity = _rb.velocity;	
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;

		_rb.AddForce(velocityChange, m_currentForceMode);

	}

	bool CheckGround(){
		bool grounded;
		if (_rb.velocity.y > -0.001f && _rb.velocity.y < 0.001f) {			
			grounded = true;
		} else {			
			grounded = false;
		}
		return grounded;
	}

	void ChangeDirection(){
		float x = m_player.transform.position.x - gameObject.transform.position.x;
		if(x > 0){
			walkingDirection = 1;
		}
		if(x < 0){
			walkingDirection = -1;
		}
	}


	public void DecreaseLife(float damage){
		m_enemyLife -= damage;
		if(m_enemyLife <= 0){			
			Debug.Log("Killed");
            playerScore.sumScore(scoreToDestroy);
            Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
            if (col.gameObject.GetComponent<PlayerController>().isOnBall == true)
            {
                Destroy(gameObject);
            }
		}
	}
}