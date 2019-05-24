using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawn;
    [SerializeField] CameraCtrl cameraCtrl;
    [SerializeField] float      levelTime;

    public static LevelManager instance;

    float       timer;
    GameObject  playerObject;

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
        Respawn();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > levelTime)
        {
            LoseLife();
            timer = 0;
        }
    }

    public void LoseLife()
    {
        GameMng.instance.LoseLife();

        if (GameMng.instance.GetCurrentLives() <= 0)
        {
            // Game over menu
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // Respawn
            Respawn();
        }
    }

    void Respawn()
    {
        if (playerObject != null)
        {
            Destroy(playerObject);
        }

        playerObject = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);

        cameraCtrl.target = playerObject.transform;

        timer = 0.0f;
    }

    public float GetCurrentTimePercentage()
    {
        return 1.0f - (timer / levelTime);
    }
}
