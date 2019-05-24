using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image[]         livesImages;
    [SerializeField] Image           clockImage;

    public static IngameUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        UpdateLivesDisplay();
        UpdateScoreDisplay(0);
    }

    private void Update()
    {
        UpdateTimeDisplay();
    }

    public void UpdateLivesDisplay()
    {
//        livesText.text = "Lives: " + GameMng.instance.GetCurrentLives();
        for (int i = 0; i < livesImages.Length; i++)
        {
            if (i < GameMng.instance.GetCurrentLives()) livesImages[i].enabled = true;
            else livesImages[i].enabled = false;
        }
    }

    void UpdateTimeDisplay()
    {
        clockImage.fillAmount = LevelManager.instance.GetCurrentTimePercentage();
    }

    public void UpdateScoreDisplay(int score)
    {
        scoreText.text = "Score\n" + score;
    }
}
