using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    private int starting = 0;
   

    public int currentScore;
    public int currentCoin;

    void Awake()
    {
        currentScore = starting;
        currentCoin = starting;
    }

    public void sumScore(int score)
    {
        currentScore += score;
    }

    public void sumCoin(int scoin)
    {
        currentCoin += scoin;
    }
}
