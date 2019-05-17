using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMng : MonoBehaviour
{
    [SerializeField] int                maxLives;
    [SerializeField] GameObject         playerPrefab;
    [SerializeField] Transform          spawnPoint;
    [SerializeField] CameraCtrl         cameraCtrl;

    int currentLives;

    public static GameMng instance;

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
        currentLives = maxLives;
        IngameUI.instance.UpdateLivesDisplay();

        Respawn();
    }

    public void LoseLife()
    {
        currentLives--;
        IngameUI.instance.UpdateLivesDisplay();

        if (currentLives <= 0)
        {
            // Fazemos cenas
        }
        else
        {
            Respawn();
        }
    }

    void Respawn()
    { 
        // Respawn do jogador
        GameObject playerObj = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        cameraCtrl.target = playerObj.transform;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}
