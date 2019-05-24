using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class GameMng : MonoBehaviour
{
    [SerializeField] int                maxLives;

    int currentLives;
    int currentScore = 0;
    int highScore = 0;

    public static GameMng instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
#endif
        }
    }

    public void LoseLife()
    {
        currentLives--;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public void AddScore(int score)
    {
        currentScore += score;

        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetHighscore()
    {
        return highScore;
    }

    public void ResetGame()
    {
        currentScore = 0;
        currentLives = maxLives;
    }
}
