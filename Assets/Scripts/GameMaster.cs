using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public int score;
    public Text scoreText;
    private void Start()
    {
        score = 0;
        scoreText.fontSize = 85;
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        score += 1;
        scoreText.text = "Score:" + score.ToString();
    }
}
