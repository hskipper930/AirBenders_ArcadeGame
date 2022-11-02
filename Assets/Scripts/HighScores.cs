using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "HighScores")]
public class HighScores : ScriptableObject
{
    private int[] scores = new int[10];
    private Dictionary<string, int> playerNames = new Dictionary<string, int>();

    public bool CheckForHighScore(int score)
    {
        foreach(int highScore in scores)
        {
            if (score > highScore)
            {
                return true;
            }
        }
        return false;
    }

    public void AddHighScore(string name, int score)
    {
        scores[0] = score;
        Array.Sort(scores);
        playerNames[name] = score;
    }
}
