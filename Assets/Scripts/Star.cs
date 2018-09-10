using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    private GameObject player;
    private AudioSource _audio;
    private LevelManager _levelManager;
    public GameObject star;

    public int scoreToDestroy = 100;
    public AudioClip starSound;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _levelManager = FindObjectOfType<LevelManager>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 4.0f);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _audio.clip = starSound;
            _audio.Play();
            _levelManager.sumScore(scoreToDestroy);
            _levelManager.sumLife(1);

            gameObject.GetComponent<BoxCollider>().enabled = false;
            star.GetComponent<MeshRenderer>().enabled = false;
            
            Destroy(gameObject, 2.0f);

        }

    }
}
