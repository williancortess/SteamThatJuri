using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManagement : MonoBehaviour {

    public static DataManagement dataManagement;

    public int highScore;
    public int coinsCollected;
    public int starsCollected;
    public int life;
    public string levelName;

    public double playerPositionX = 1;
    public double playerPositionY = 1;

    private LevelManager levelManager;
    public Transform player;
    // Use this for initialization
    void Awake()
    {
        if (dataManagement == null)
        {
            DontDestroyOnLoad(gameObject);
            dataManagement = this;
        }
        else if (dataManagement != this)
        {
            Destroy(gameObject);
        }

    }

    public void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void SaveData()
    {
        BinaryFormatter binForm = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInf.dat");
        gameData data = new gameData();
        data.highScore = highScore;
        data.coinsCollected = coinsCollected;
        data.starsCollected = starsCollected;
        data.life = life;
        data.levelName = levelName;

        data.playerPositionX = playerPositionX;
        data.playerPositionY = playerPositionY;

        binForm.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInf.dat"))
        {
            BinaryFormatter binForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInf.dat", FileMode.Open);
            gameData data = (gameData)binForm.Deserialize(file);
            file.Close();
            highScore = data.highScore;
            coinsCollected = data.coinsCollected;
            starsCollected = data.starsCollected;
            
            if(data.levelName == SceneManager.GetActiveScene().name)
            {
                playerPositionX = data.playerPositionX;
                playerPositionY = data.playerPositionY;
            }
            else
            {
                playerPositionX = 10.61;
                playerPositionY = 1.26;
            }
            
            life = data.life;
            levelName = data.levelName;

            levelManager.currentScore = highScore;
            levelManager.currentCoin = coinsCollected;
            levelManager.currentStar = starsCollected;

            if (player != null)
            {
                player.transform.position = new Vector3((float)playerPositionX, (float)playerPositionY, 0);
            }

            if (levelManager.scoreText != null)
            {
                levelManager.scoreText.text = "Score: " + highScore.ToString();
            }


        }
    }

    [Serializable]
    class gameData
    {
        public int highScore;
        public int coinsCollected;
        public int starsCollected;
        public int life;
        public string levelName;

        public double playerPositionX;
        public double playerPositionY;
    }
}
