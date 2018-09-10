using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject levelButtonPrefab;
	public GameObject levelButtonContainer;

	public GameObject shopButtonPrefab;
	public GameObject shopButtonContainer;

	public Material playerMaterial;

	private Transform cameraTransform;
	private Transform cameraDesiredLookAt;

    private Loading loading;

	void Start(){
        loading = GameObject.FindGameObjectWithTag("Loading").GetComponent<Loading>();
        

        cameraTransform = Camera.main.transform;
		Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
		foreach (Sprite thumbnail in thumbnails) {
			GameObject container = Instantiate (levelButtonPrefab) as GameObject;
			container.GetComponent<Image>().sprite = thumbnail;
			container.transform.SetParent(levelButtonContainer.transform, false);

			string sceneName = thumbnail.name;
            //string sceneName = "Loading";
            container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
		}
	}

	void Update(){
		if(cameraDesiredLookAt != null)
			cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, 3 * Time.deltaTime);
	}

	private void LoadLevel(string sceneName){
        Debug.Log("LOAD LEVEL  " + sceneName);
        loading.Carrega(sceneName);

        //SceneManager.LoadScene(sceneName);
	}

	public void LookAtMenu(Transform menuTransform){
		cameraDesiredLookAt = menuTransform;
		//Camera.main.transform.LookAt(menuTransform.position);
	}

	private void ChangePlayerArmor(int index){
		Debug.Log(index);
		if(index == 0){
			playerMaterial.color = Color.blue;
		}
		else if(index == 1){
			playerMaterial.color = Color.red;
		}
		else if(index == 2){
			playerMaterial.color = Color.magenta;
		}
	}
}
