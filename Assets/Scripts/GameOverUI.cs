using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScoreText.text = "Highest Score: " + GameMng.instance.GetHighscore();
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
