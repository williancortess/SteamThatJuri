using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public enum EnemyState { InTarget, Patrolling }
    public Transform m_player;

    float _speed = 350.0f;
    float _jumpForce = 7.0f;
    float _maxVelocityChange = 10.0f;
    float _walkingDirection = 1.0f;

    int _enemyLife = 2;
    Rigidbody _rb;
    ForceMode _currentForceMode;
    EnemyState _currState;

    bool _player = false;
    bool _inTarget = true;
    bool _walkingRight = true;

    GameObject player;
    GameObject detectedPlayer;
    DetectedPlayer detectedPlayerSc;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        detectedPlayer = GameObject.FindGameObjectWithTag("DetectedPlayer");
        detectedPlayerSc = detectedPlayer.GetComponent<DetectedPlayer>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _currState = EnemyState.InTarget;
        _currentForceMode = ForceMode.VelocityChange;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateGameState();
    }

    void EnterGameState(EnemyState newState)
    {
        LeaveGameState();

        _currState = newState;
        switch (_currState)
        {
            case EnemyState.Patrolling:
                {
                    //Debug.Log("Enter Patrolling");
                }
                break;

            case EnemyState.InTarget:
                {
                    //Debug.Log("Enter InTarget");
                }
                break;
        }
    }

    //Actions when the player leave a GameState;
    void LeaveGameState()
    {
        switch (_currState)
        {
            case EnemyState.Patrolling:
                {
                    //Debug.Log("Leave Patrolling");
                }
                break;

            case EnemyState.InTarget:
                {
                    //Debug.Log("Leave InTarget");
                }
                break;
        }
    }

    void UpdateGameState()
    {
        switch (_currState)
        {
            case EnemyState.Patrolling:
                {
                    //CheckGround();
                    if (CheckGround())
                    {
                        ChangeDirection();
                        Move(_walkingDirection);

                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, Vector3.right, out hit, 5.0f))
                        {
                            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                        }
                        else if (Physics.Raycast(transform.position, Vector3.left, out hit, 5.0f))
                        {
                            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                        }
                    }
                }
                break;

            case EnemyState.InTarget:
                {
                    Move(_walkingDirection);

                    if (detectedPlayerSc.playerDetected)
                    {
                        _inTarget = false;
                        _currState = EnemyState.Patrolling;
                    }
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject == player)
        //{
        //    _inTarget = false;
        //}


        if (!_walkingRight && other.gameObject.tag == "TriggerL")
        {
            _walkingDirection = 1;
            _walkingRight = true;
        }
        else if (_walkingRight && other.gameObject.tag == "TriggerR")
        {
            _walkingDirection = -1;
            _walkingRight = false;
        }
    }

    void Move(float direction)
    {
        Vector3 targetVelocity = new Vector3(direction, 0, 0);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= _speed * Time.deltaTime;

        Vector3 velocity = _rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -_maxVelocityChange, _maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -_maxVelocityChange, _maxVelocityChange);
        velocityChange.y = 0;

        _rb.AddForce(velocityChange, _currentForceMode);

    }

    bool CheckGround()
    {
        bool grounded;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y + 0.001f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        return grounded;
    }

    void ChangeDirection()
    {

        float x = m_player.transform.position.x - gameObject.transform.position.x;

        if (x > 0)
        {
            _walkingDirection = 1;
        }
        if (x < 0)
        {
            _walkingDirection = -1;
        }
    }

}


//public void DecreaseLife(int damage){
//	m_enemyLife -= damage;
//	if(m_enemyLife <= 0){			
//		Debug.Log(m_enemyLife);
//		Destroy(gameObject);
//	}
//}
//}