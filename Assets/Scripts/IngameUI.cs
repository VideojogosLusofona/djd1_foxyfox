using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;

    void Update()
    {
        UpdateLifeDisplay();
    }

    void UpdateLifeDisplay()
    {
        livesText.text = "Lives: " + GameMng.instance.GetCurrentLives();
    }
}
