using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    private float score;
    public Text text;
    private float tempYPos;
    public float scoreToAdd;

    private void Update()
    {
        AddScore();
        DisplayScore();
    }

    private void DisplayScore()
    {
        int currentScore = (int)score;
        text.text = score.ToString("00.00");
    }

    private void AddScore()
    {
        float currentPos = gameObject.transform.position.y;
        if (tempYPos < currentPos)
        {
            score += scoreToAdd * Time.deltaTime;
        }
        tempYPos = transform.position.y;
    }
}
