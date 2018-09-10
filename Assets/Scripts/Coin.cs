using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    private GameObject player;
	private AudioSource _audio;
	private LevelManager playerScore;
	public GameObject pillow;

    public int scoreToDestroy = 5;

    public AudioClip coinSound;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		playerScore = FindObjectOfType<LevelManager>();
		_audio = GetComponent<AudioSource>();
    }

	void Update(){
		transform.RotateAround (transform.position, Vector3.up, 5.0f); 
	}
		

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			_audio.clip = coinSound;
			_audio.Play();	
            playerScore.sumScore(scoreToDestroy);
            playerScore.sumCoin(1);       

			gameObject.GetComponent<BoxCollider>().enabled = false;
			pillow.GetComponent<MeshRenderer>().enabled = false;

			Destroy(gameObject, 2.0f);

        }

    }
}
