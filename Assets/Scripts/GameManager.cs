using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public float worldSpeed = 1;
    public Text scoreText;
    public GameObject gameOverPanel;
    float score; 
    int coins;

    private void Update()
    {
        score += worldSpeed * Time.deltaTime;
        var nfi = new NumberFormatInfo()
        {
            NumberGroupSeparator = " "
        };
        scoreText.text = score.ToString("N0", nfi);
    }

    internal void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true); //do w³¹czania/wy³¹czania gameObjectó
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    public void CoinCollected()
    {
        coins++;
    }
}
