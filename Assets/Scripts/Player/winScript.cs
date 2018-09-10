using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class winScript : MonoBehaviour {

	public float speed = 60f;
	
	void Update () {
		transform.RotateAround (transform.position, Vector3.up, speed); 
	}

	void OnTriggerEnter() {
        Vector3 pos = transform.position;
        DataManagement.dataManagement.playerPositionX = 10.61;
        DataManagement.dataManagement.playerPositionY = 1.26;

        DataManagement.dataManagement.SaveData();

        SceneManager.LoadScene ("Menu");
	}
}
