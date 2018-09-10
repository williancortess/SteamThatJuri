using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject currentCheckpoint;
	public int currentScore;
	public int currentCoin;
    public int currentStar;
    public int life;
	public Text scoreText;
	public Text lifeText;
    public string levelName;

	private PlayerController _player;
    public Transform player;
    private int starting = 0;

    private double playerPositionX;
    private double playerPositionY;

    void Start () {
        Debug.Log("Enter");
		pauseMenu.SetActive(false);
        Debug.Log(pauseMenu.activeSelf);
        _player = FindObjectOfType<PlayerController>();
        currentScore = DataManagement.dataManagement.highScore;
        currentCoin = DataManagement.dataManagement.coinsCollected;
        currentStar = DataManagement.dataManagement.starsCollected;
        playerPositionX = DataManagement.dataManagement.playerPositionX;
        playerPositionY = DataManagement.dataManagement.playerPositionY;
        levelName = DataManagement.dataManagement.levelName;

        if (DataManagement.dataManagement.life > 0)
        {
            life = DataManagement.dataManagement.life;
            DataManagement.dataManagement.SaveData();
        }
        else
        {
            DataManagement.dataManagement.life = 2;
            DataManagement.dataManagement.SaveData();
        }
        

        player.transform.position = new Vector3((float)playerPositionX, (float)playerPositionY, 0);
        scoreText.text = "Score: " + currentScore.ToString();
		lifeText.text = "Life " + life.ToString();

    }

    public void ResetData()
    {
        DataManagement.dataManagement.highScore         = 0;
        DataManagement.dataManagement.coinsCollected    = 0;
        DataManagement.dataManagement.starsCollected    = 0;
        DataManagement.dataManagement.playerPositionX   = 10.61;
        DataManagement.dataManagement.playerPositionY   = 1.26;
        DataManagement.dataManagement.life = 2;
        DataManagement.dataManagement.levelName = SceneManager.GetActiveScene().name;
        DataManagement.dataManagement.SaveData();

        SceneManager.LoadScene("GameOver");
    }

	void Update () {
	
	}

	public void ToMenu(){		
		Time.timeScale = 1;
		SceneManager.LoadScene("Menu");
	}

	public void TogglePauseMenu(){
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		if(pauseMenu.activeSelf){
			Time.timeScale = 0;
		}
		else{
			Time.timeScale = 1;
		}
	}
	public void RespawnPlayer(){
        DataManagement.dataManagement.LoadData();
        currentScore = DataManagement.dataManagement.highScore;
        currentCoin = DataManagement.dataManagement.coinsCollected;
        currentStar = DataManagement.dataManagement.starsCollected;

        playerPositionX = DataManagement.dataManagement.playerPositionX;
        playerPositionY = DataManagement.dataManagement.playerPositionY;

        if (player != null)
        {
            player.transform.position = new Vector3((float)playerPositionX, (float)playerPositionY, 0);
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }

        //SceneManager.LoadScene(0);
        //_player.transform.position = currentCheckpoint.transform.position;
    }

    public void sumLife(int life)
    {
        DataManagement.dataManagement.life += life;
		this.life = DataManagement.dataManagement.life;
		lifeText.text = "Life " + this.life.ToString();

        if(DataManagement.dataManagement.life < 0)
        {
            ResetData();
        }

        Debug.Log("  DB   " + DataManagement.dataManagement.life);
    }

    public void sumScore(int score)
	{
        DataManagement.dataManagement.highScore += score;
        currentScore += score;
		scoreText.text = "Score: " + currentScore.ToString();
	}

	public void sumCoin(int scoin)
	{
        DataManagement.dataManagement.coinsCollected++;
        currentCoin += scoin;
	}

    public void sumStar()
    {
        DataManagement.dataManagement.starsCollected++;
        currentStar += 1;
    }
}
