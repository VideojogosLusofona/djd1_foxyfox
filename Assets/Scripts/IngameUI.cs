using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;

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
    }

    public void UpdateLivesDisplay()
    {
        livesText.text = "Lives: " + GameMng.instance.GetCurrentLives();
    }
}
