using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public Image currentHealthBar;

	public float maximumHealth = 100;
	public float currentHealth;
	public AudioClip hitPain;
    //public int Life;

	private AudioSource _audio;
	private LevelManager _levelManager;

    void Awake()
    {
        currentHealth = maximumHealth;
    }

	void Start(){
		_audio = GetComponent<AudioSource>();
		_levelManager = FindObjectOfType<LevelManager>();
        
	}

	void Update(){	

		float ratio = currentHealth/ maximumHealth;
		currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);		
	}
		

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
		_audio.clip = hitPain;
		_audio.Play();	
        if(currentHealth <= 0)
        {
            //Life = Life - 1;
            _levelManager.sumLife(-1);
            DataManagement.dataManagement.SaveData();

            _levelManager.RespawnPlayer();
			currentHealth = maximumHealth;
			//SceneManager.LoadScene (0);
        }
    }
}
