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
    public Image scorePanel;
    public Image highScoreFill;
    public Material burnUImaterial;
    public Material burnUImaterialFill;
    public Text coinText;
    public GameObject gameOverPanel;
    float score;
    float highScore;
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
        highScoreFill.fillAmount = score / highScore;

        scorePanel.material = null;
        highScoreFill.material = null;
    }

    private void Update()
    {
        score += worldSpeed * Time.deltaTime;
        scoreText.text = ((int)score).ToString();

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreFill.material = burnUImaterialFill;
            scorePanel.material = burnUImaterial;
            scorePanel.color = Color.white;
        }
        highScoreFill.fillAmount = score / highScore;
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
            CancelImmortality();
            CancelInvoke(nameof(CancelImmortality));
        }
        else
        {
            effectsController.BatteryEnable(true);
        }
        ImmortalityActive = true;
        worldSpeed += immortality.SpeedBoost;
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
