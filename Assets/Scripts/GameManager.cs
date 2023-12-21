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

    public AudioClip gameOverSfx;
    public AudioClip restartSfx;

    bool highScoreReached;
    public Action OnHighScoreReached;

    private void Start()
    {
        Application.targetFrameRate = 60;
        //PlayerPrefs.DeleteKey("HighScore");

        gameOverPanel.SetActive(false);

        immortality.IsActive = false;
        magnet.IsActive = false;

        coins = PlayerPrefs.GetInt("Coins");
        coinText.text = coins.ToString();

        highScore = PlayerPrefs.GetFloat("HighScore");

    }

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
    bool over;
    public event Action GameOverEvent;
    internal void GameOver()
    {
        if (immortality.IsActive || over) return;

        //Time.timeScale = 0;
        worldSpeed = 0;
        GameOverEvent?.Invoke();
        gameOverPanel.SetActive(true);

        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySfx(gameOverSfx);
    }

    public void Restart()
    {
        SoundManager.Instance.PlayUI(restartSfx);
        SoundManager.Instance.RestartMusic();
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
