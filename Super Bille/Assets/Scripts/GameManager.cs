using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return Instance;
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

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        AddScore(0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void AddScore(int amount)
    {
        _score += amount;
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
        }
        else if(GameState == GameStates.InGame)
        {
            ChangeGameState(GameStates.Pause);
        }
    }

    public void ChangeGameState(GameStates gameState)
    {
        _gameState = gameState;
        switch(GameState)
        {
            case GameStates.MainMenu:
                UIManager.Instance.ChangeUI((int)GameState);
                break;
            case GameStates.InGame:
                UIManager.Instance.ChangeUI((int)GameState);
                SceneManager.LoadScene("MainScene");
                Time.timeScale = 1;
                break;
            case GameStates.Pause:
                UIManager.Instance.ChangeUI((int)GameState);
                Time.timeScale = 0;
                break;
            case GameStates.GameOver:
                UIManager.Instance.score.text = "Score : " + score + " m";
                UIManager.Instance.bestScore.text = "Best Score : " + bestScore + " m";
                ResetScore();
                UIManager.Instance.ChangeUI((int)GameState);
                break;
        }
    }

    public void ChangeGameStateByUI(int gameState)
    {
        ChangeGameState((GameStates)gameState);
    }
}
