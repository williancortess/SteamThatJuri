using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    
	void Start () {
        StartCoroutine(CenaDeCarregamento("Menu"));
    }

    IEnumerator CenaDeCarregamento(string cena)
    {

        //AsyncOperation carregamento = Application.LoadLevelAsync(cena);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(cena);
    }
}
