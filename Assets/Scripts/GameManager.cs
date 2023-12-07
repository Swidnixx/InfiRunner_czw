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
    [SerializeField] private PowerupManager powerupManager;
    public MagnetSO Magnet => powerupManager.Magnet;
    public ImmortalitySO Immortality => powerupManager.Immortality;
    public bool MagnetActive { get => Magnet.IsActive; set => Magnet.IsActive = value; }
    public bool ImmortalityActive { get => Immortality.IsActive; set => Immortality.IsActive = value; }

    private void Start()
    {
        Immortality.IsActive = false;
        Magnet.IsActive = false;

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
        if (Immortality.IsActive) return;

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
        Invoke(nameof(CancelMagnet), Magnet.Duration);
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
        worldSpeed += Immortality.SpeedBoost;
        Invoke(nameof(CancelImmortality), Immortality.Duration);
    }
    private void CancelImmortality()
    {
        worldSpeed -= Immortality.SpeedBoost;
        ImmortalityActive = false;
    }
}
