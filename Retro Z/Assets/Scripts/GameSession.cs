using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{

    int currentScore = 0;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    { 
            int gameStatusCount = FindObjectsOfType<GameSession>().Length;

            if (gameStatusCount > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void AddToScore(int pointsPerZombieKilled)
    {
        currentScore += pointsPerZombieKilled;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}
