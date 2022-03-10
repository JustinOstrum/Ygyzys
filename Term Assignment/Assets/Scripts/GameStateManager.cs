using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public int Collectibles;
    public int Lives;

    public string startingLevel;
    public string GAMEOVER;

    PlayerController player;

    //The one and only GameStateManager
    //"singleton"
    private static GameStateManager instance;

    public static GameStateManager Instance
    {
        get { return instance; }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void changeCollectibles(int deltaCollectibles)
    {
        Collectibles += deltaCollectibles;
    }

    public void changeLives(int deltaLives)
    {
        Lives += deltaLives;
    }

    public void onStart()
    {
        SceneManager.LoadScene(startingLevel);
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void onDeath()
    {
        changeLives(-1);
        if (Lives < 0)
        {
            Debug.Log("No More Lives!");
            Collectibles = 0;
            Lives = 3;
            SceneManager.LoadScene(GAMEOVER);
        }
    }

    public void quitGame()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    public void onGoal(string nextScene)
    {
        if (nextScene == "default")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
