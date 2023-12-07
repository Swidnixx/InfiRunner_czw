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
    public float score { get; private set; }
    public float highScore { get; private set; }
    int coins;

    //Powerups
    public PowerupManager powerupManager;
    public MagnetSO magnet => powerupManager.Magnet;
    public ImmortalitySO immortality => powerupManager.Immortality;
    public bool MagnetActive { get => magnet.IsActive; set => magnet.IsActive = value; }
    public bool ImmortalityActive { get => immortality.IsActive; set => immortality.IsActive = value; }
   
    public PowerupPanelUI powerupsUI;

    public EffectsController effectsController;

    private void Start()
    {
        //PlayerPrefs.DeleteKey("HighScore");

        immortality.IsActive = false;
        magnet.IsActive = false;

        coins = PlayerPrefs.GetInt("Coins");
        coinText.text = coins.ToString();

        highScore = PlayerPrefs.GetFloat("HighScore");

    }

    bool highScoreReached;
    public Action OnHighScoreReached;
    private void Update()
    {
        score += worldSpeed * Time.deltaTime;
        scoreText.text = ((int)score).ToString();

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);

            if(!highScoreReached)
            {
                highScoreReached = true;
                OnHighScoreReached?.Invoke();
            }
        }
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
        powerupsUI.ActivateMagnet(magnet.Duration);
    }
    private void CancelMagnet()
    {
        MagnetActive = false;
    }

    public void ImmortalityCollected()
    {
        if(ImmortalityActive)
        {
           // CancelImmortality();
            CancelInvoke(nameof(CancelImmortality));
        }
        else
        {
            worldSpeed += immortality.SpeedBoost;
            effectsController.BatteryEnable(true);
        }

        ImmortalityActive = true;
        Invoke(nameof(CancelImmortality), immortality.Duration);
        powerupsUI.ActivateBattery(immortality.Duration);
    }
    private void CancelImmortality()
    {
        worldSpeed -= immortality.SpeedBoost;
        ImmortalityActive = false;
        effectsController.BatteryEnable(false);
    }
}
