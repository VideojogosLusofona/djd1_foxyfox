using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameMng : MonoBehaviour
{
    [SerializeField] int                maxLives;

    int         currentLives;
    int         currentScore;
    int         highScore = 0;

    public static GameMng instance;

    void Awake()
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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            EditorApplication.isPaused = true;
        }
#endif
    }

    public void LoseLife()
    {
        currentLives--;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public void ResetGame()
    {
        currentScore = 0;
        currentLives = maxLives;
    }

    public void AddScore(int s)
    {
        currentScore += s;

        IngameUI.instance.UpdateScoreDisplay(currentScore);
        UpdateHighscore();
    }

    public void UpdateHighscore()
    {
        if (highScore < currentScore)
        {
            highScore = currentScore;
        }
    }

    public int GetHighscore()
    {
        return highScore;
    }
}
