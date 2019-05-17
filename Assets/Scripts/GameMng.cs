using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMng : MonoBehaviour
{
    [SerializeField] int                maxLives;
    [SerializeField] GameObject         playerPrefab;
    [SerializeField] Transform          playerSpawn;
    [SerializeField] CameraCtrl         cameraCtrl;

    int currentLives;

    public static GameMng instance;

    private void Awake()
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
    }

    public void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            // Game over menu
        }
        else
        {
            // Respawn
            Respawn();
        }
    }

    void Respawn()
    {
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);

        cameraCtrl.target = newPlayer.transform;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}
