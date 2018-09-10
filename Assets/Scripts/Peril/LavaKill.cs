using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LavaKill : MonoBehaviour {

    private LevelManager _levelManager;

    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerStay (Collider col) {
		if (col.tag == "Player") {
            _levelManager.RespawnPlayer();
            //SceneManager.LoadScene (0);
		}
	}
}
