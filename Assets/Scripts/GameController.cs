using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //fields
    private int score;
    public GUIText scoreText;


	// Use this for initialization
	void Start () {
        score = 0;
        UpdateScore();
	}

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }



    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
