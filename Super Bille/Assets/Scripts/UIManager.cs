using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject mainMenuGO;
    public GameObject hudGO;
    public GameObject pauseGO;
    public GameObject gameOverGO;
    public Text scoreHUD;
    public Text score;
    public Text bestScore;

    private void Awake()
    {
        _instance = this;
    }
    public void ChangeUI(int gameStateInt)
    {
        switch(gameStateInt)
        {
            case 0:
                mainMenuGO.SetActive(true);
                hudGO.SetActive(false);
                pauseGO.SetActive(false);
                gameOverGO.SetActive(false);
                break;
            case 1:
                mainMenuGO.SetActive(false);
                hudGO.SetActive(true);
                pauseGO.SetActive(false);
                gameOverGO.SetActive(false);
                break;
            case 2:
                mainMenuGO.SetActive(false);
                hudGO.SetActive(false);
                pauseGO.SetActive(true);
                gameOverGO.SetActive(false);
                break;
            case 3:
                mainMenuGO.SetActive(false);
                hudGO.SetActive(false);
                pauseGO.SetActive(false);
                gameOverGO.SetActive(true);
                break;
        }
    }
}
