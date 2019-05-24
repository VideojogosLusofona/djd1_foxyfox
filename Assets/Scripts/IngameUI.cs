using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] Image[]         livesImages;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image           timeImage;

    void Update()
    {
        UpdateLifeDisplay();
        UpdateScoreDisplay();
        UpdateTimeDisplay();
    }

    void UpdateLifeDisplay()
    {
        livesText.text = "Lives: " + GameMng.instance.GetCurrentLives();

        for (int i = 0; i < livesImages.Length; i++)
        {
            //            if (i < GameMng.instance.GetCurrentLives()) livesImages[i].enabled = true;
            //            else livesImages[i].enabled = false;

            livesImages[i].enabled = (i < GameMng.instance.GetCurrentLives());
        }
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score\n" + GameMng.instance.GetCurrentScore();
    }

    void UpdateTimeDisplay()
    {
        timeImage.fillAmount = LevelManager.instance.GetCurrentTimePercentage();
    }
}
