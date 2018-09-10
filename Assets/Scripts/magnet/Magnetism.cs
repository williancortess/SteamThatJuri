using UnityEngine;
using System.Collections;

public class Magnetism : MonoBehaviour {
	public Transform[] target;
    public GameObject[] steams;
    public GameObject steam;
    public float speed;
	public EnterExitScript enter;
    public CameraControler camera;

	private AudioSource _audio;
    private bool control = true;
	private int _current = 1;
	private bool _active = false;
	private int _check = 1;
	private Vector3 _pos;
	private Transform _player;


	void Start(){
		_audio = GetComponent<AudioSource>();
		_player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
		if(_active){				
			MoveFoward();		
		}
	}

	public void EndMagnet(){
		if((enter.begin == true) && _check == 2){
			_active = false;
			_check = 1; 
			_player.GetComponent<CapsuleCollider>().isTrigger = false;
            if (_current >= 4)
            {
                _player.position = steams[0].transform.position;
            }
            else
            {
                _player.position = steams[_current].transform.position;
            }
            camera.target = _player.gameObject;
			_player.GetComponent<Rigidbody>().AddForce(ChooseDiretion() * 1500, ForceMode.Impulse);
			_player.GetComponent<Rigidbody>().drag = 0;
			_player.GetComponent<PlayerController>().SmokeBall.SetActive(true);
            _current = 0;
            _audio.Stop();
          
        }
	}



	void MoveFoward(){
        if (control)
        {
            ActivateSteam();
            StartCoroutine(WaitSteam());
        }
	}
    IEnumerator WaitSteam(){
        control = false;
        yield return new WaitForSeconds(0.4f);
        if (_current >= 4)
        {
            _current = 0;
        }
        _current += 1;
        control = true;
        

    }

	Vector3 ChooseDiretion(){
		switch (_current) {
		case 1:
			return Vector3.up;	
		case 2:
			return Vector3.left;			
		case 3:
			return Vector3.down;			
		default:
			return Vector3.right;
		}
	}

    void ActivateSteam()
    {
        switch (_current)
        {
            case 1:
                GameObject prefab = (GameObject)Instantiate(steam, steams[1].transform.position, steams[1].transform.rotation);
                prefab.transform.rotation = Quaternion.LookRotation(Vector3.up);
                break;
            case 2:
                GameObject prefab1 = (GameObject)Instantiate(steam, steams[2].transform.position, steams[2].transform.rotation);
                prefab1.transform.rotation = Quaternion.LookRotation(Vector3.left);
                break;
            case 3:
                GameObject prefab2 = (GameObject)Instantiate(steam, steams[3].transform.position, steams[3].transform.rotation);
                prefab2.transform.rotation = Quaternion.LookRotation(Vector3.down);
                break;
            default:
                GameObject prefab3 = (GameObject)Instantiate(steam, steams[0].transform.position, steams[0].transform.rotation);
                prefab3.transform.rotation = Quaternion.LookRotation(Vector3.right);
                break;
        }
    }

    void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player" && (enter.begin == true) && _check == 1 && _current != 0){            
            Debug.Log(_current);
            _active = true;
            _current = 0;
            _check = 2;
            camera.target = gameObject;
            _player.GetComponent<Rigidbody>().useGravity = false;
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;            
            _player.GetComponent<PlayerController>().isOnBall = true;
            _player.GetComponent<PlayerController>().WaitForShit = false;
			_player.GetComponent<PlayerController>().SmokeBall.SetActive(false);
            _player.GetComponent<PlayerController>().magnetism = this.gameObject.GetComponent<Magnetism>();
            _player.GetComponent<Rigidbody>().drag = 1;
            _player.GetComponent<CapsuleCollider>().isTrigger = true;

            _player.transform.position = target[0].position;

            _audio.Play();          
		}
	}
}