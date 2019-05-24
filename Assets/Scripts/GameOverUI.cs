using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI    highScoreText;

    void Start()
    {
        highScoreText.text = "Highscore: " + GameMng.instance.GetHighscore();
    }

    public void Retry()
    {
        GameMng.instance.ResetGame();
        SceneManager.LoadScene("Level1");       
    }

    public void Quit()
    {
        Application.Quit();
    }
}
