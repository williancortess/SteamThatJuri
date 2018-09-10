using UnityEngine;
using System.Collections;

public class ThwompAI : MonoBehaviour {
    public enum EnemyState { Stopped, Down, Up }

    Rigidbody _rb;
    ForceMode _currentForceMode;
    EnemyState _currState;
   
    public DetectedPlayerThwomp detectedPlayer;

    bool detectedThwompUp = false;
    bool detectedThwompDown = false;
   
    public float _speedUp = 600f;
    public float _speedDown = 1500f;

    float _walkingDirection = 1.0f;
    float _time = 0.0f;

    void Start () {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _currState = EnemyState.Stopped;
    }
	
	void Update () {
        UpdateGameState();
    }

    void UpdateGameState()
    {
        switch (_currState)
        {
            case EnemyState.Stopped:
                {
                    if (detectedPlayer.playerDetected)
                    {
                        Debug.Log("PLAYER ENCONTRADO");
                        _currState = EnemyState.Down;
                    }
                }
                break;

            case EnemyState.Down:
                {
                    if (detectedThwompDown == false)
                    {
                        Move(-1, 0, _speedDown);
                    }
                    else
                    {
                       // _rb.isKinematic = true;
                        _time += Time.deltaTime;
                        if (_time >= 2)
                        {
                            _time = 0;
                            //_rb.isKinematic = false;
                            _currState = EnemyState.Up;
                        }
                    }
                    
                }
                break;

            case EnemyState.Up:
                {
                    if (detectedThwompUp == false)
                    {
                        Move(1, 0, _speedUp);
                    }else
                    {
                        _currState = EnemyState.Stopped;
                    }
                }
                break;
        }
    }

    void EnterGameState(EnemyState newState)
    {
        LeaveGameState();

        _currState = newState;
        switch (_currState)
        {
            case EnemyState.Stopped:
                {
                }
                break;

            case EnemyState.Down:
                {
                }
                break;

            case EnemyState.Up:
                {
                }
                break;
        }
    }
    
    void LeaveGameState()
    {
        switch (_currState)
        {
            case EnemyState.Stopped:
                {
                }
                break;

            case EnemyState.Down:
                {
                }
                break;

            case EnemyState.Up:
                {
                }
                break;
        }
    }

    void Move(float direction, float maxVelocityChange, float speed)
    {
        transform.Translate(0, direction * speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TriggerUp")
        {
            detectedThwompUp = true;
        }

        if (other.gameObject.tag == "TriggerDown")
        {
            detectedThwompDown = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TriggerUp")
        {
            detectedThwompUp = false;
        }

        if (other.gameObject.tag == "TriggerDown")
        {
            detectedThwompDown = false;
        }
    }
}
