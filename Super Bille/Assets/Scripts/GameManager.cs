using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public enum GameStates
    {
        MainMenu,
        InGame,
        Pause,
        GameOver,
    }
    private static GameStates _gameState;
    public static GameStates GameState
    {
        get
        {
            return _gameState;
        }
    }

    private static int _bestScore;
    public static int bestScore
    {
        get
        {
            return _bestScore;
        }
    }
    
    private static int _score;
    public static int score
    {
        get
        {
            return _score;
        }
    }

    public AudioSource menuTheme;
    public AudioSource inGameTheme;
    public AudioSource gameOverTheme;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        AddScore(0);
        if(PlayerPrefs.HasKey("bestScore"))
        {
            _bestScore = PlayerPrefs.GetInt("bestScore");
        }
        ChangeGameState(0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGameState(GameStates.GameOver);
        }
    }
    public void AddScore(float amount)
    {
        _score = (int)amount;
        UIManager.Instance.scoreHUD.text = "Score : " + score + " m";
    }
    public void ResetScore()
    {
        if(bestScore < score)
        {
            _bestScore = score;
        }
        _score = 0;
    }

    public void PauseUnpause()
    {
        if (GameState == GameStates.Pause)
        {
            ChangeGameState(GameStates.InGame);
            inGameTheme.UnPause();
        }
        else if(GameState == GameStates.InGame)
        {
            ChangeGameState(GameStates.Pause);
            inGameTheme.Pause();
        }
    }

    public void ChangeGameState(GameStates gameState)
    {
        _gameState = gameState;
        switch(GameState)
        {
            case GameStates.MainMenu:
                UIManager.Instance.ChangeUI((int)GameState);
                PlaySound();
                break;
            case GameStates.InGame:
                UIManager.Instance.ChangeUI((int)GameState);
                if(inGameTheme.isPlaying)
                { 
                    PlaySound();
                }
                Time.timeScale = 1;
                break;
            case GameStates.Pause:
                UIManager.Instance.ChangeUI((int)GameState);
                Time.timeScale = 0;
                break;
            case GameStates.GameOver:
                Time.timeScale = 0;
                UIManager.Instance.score.text = "Score : " + score + " m";
                UIManager.Instance.bestScore.text = "Best Score : " + bestScore + " m";
                ResetScore();
                UIManager.Instance.ChangeUI((int)GameState);
                PlaySound();
                break;
        }
    }

    void PlaySound()
    {
        menuTheme.Stop();
        inGameTheme.Stop();
        gameOverTheme.Stop();
        switch((int)GameState)
        {
            case 0:
                menuTheme.Play();
                break;
            case 1:
                inGameTheme.Play();
                break;
            case 3:
                gameOverTheme.Play();
                break;
        }
    }
    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        PlayerPrefs.SetInt("bestScore", bestScore);
        Application.Quit();
    }
}
