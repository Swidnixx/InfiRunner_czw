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
    public Text coinText;
    public GameObject gameOverPanel;
    float score; 
    int coins;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        coinText.text = coins.ToString();
    }

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
        coinText.text = coins.ToString();
        PlayerPrefs.SetInt("Coins", coins);
    }

    bool magnetActive;
    float magnetTime = 10;
    float magnetDistance = 5;
    public void MagnetCollected()
    {
        if(magnetActive)
        {
            CancelInvoke(nameof(CancelMagnet)); // if magnet is already active, cancel disabling not to disable current one too early
        }
        magnetActive = true;
        Invoke(nameof(CancelMagnet), magnetTime);
    }
    private void CancelMagnet()
    {
        magnetActive = false;
    }
}
