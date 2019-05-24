using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float      maxTime = 5;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] CameraCtrl cameraCtrl;

    float       currentTime;
    GameObject  playerCharacter;

    public static LevelManager instance;

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
        currentTime = maxTime;

        Respawn();
        IngameUI.instance.UpdateLivesDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0.0f)
        {
            LoseLife();
            currentTime = maxTime;
        }
    }

    public float GetCurrentTimePercentage()
    {
        return currentTime / maxTime;
    }

    void Respawn()
    {
        if (playerCharacter != null)
        {
            Destroy(playerCharacter);
        }

        // Respawn do jogador
        playerCharacter = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        cameraCtrl.target = playerCharacter.transform;
    }

    public void LoseLife()
    {
        GameMng.instance.LoseLife();
        IngameUI.instance.UpdateLivesDisplay();

        if (GameMng.instance.GetCurrentLives() <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Respawn();
        }
    }

}
