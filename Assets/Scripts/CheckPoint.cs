using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 pos = transform.position;
            DataManagement.dataManagement.playerPositionX = pos.x;
            DataManagement.dataManagement.playerPositionY = pos.y;
            DataManagement.dataManagement.levelName = SceneManager.GetActiveScene().name;

            DataManagement.dataManagement.SaveData();
        }
    }
}
