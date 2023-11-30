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

    //Powerups
    public MagnetSO magnet;
    public ImmortalitySO immortality;
    public bool MagnetActive { get => magnet.IsActive; set => magnet.IsActive = value; }
    public bool ImmortalityActive { get => immortality.IsActive; set => immortality.IsActive = value; }

    private void Start()
    {
        immortality.IsActive = false;
        magnet.IsActive = false;

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
        if (immortality.IsActive) return;

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

    public void MagnetCollected()
    {
        if(MagnetActive)
        {
            CancelInvoke(nameof(CancelMagnet)); // if magnet is already active, cancel disabling not to disable current one too early
        }
        MagnetActive = true;
        Invoke(nameof(CancelMagnet), magnet.Duration);
    }
    private void CancelMagnet()
    {
        MagnetActive = false;
    }

    public void ImmortalityCollected()
    {
        if(ImmortalityActive)
        {
            CancelImmortality();
            CancelInvoke(nameof(CancelImmortality));
        }
        ImmortalityActive = true;
        worldSpeed += immortality.SpeedBoost;
        Invoke(nameof(CancelImmortality), immortality.Duration);
    }
    private void CancelImmortality()
    {
        worldSpeed -= immortality.SpeedBoost;
        ImmortalityActive = false;
    }
}
