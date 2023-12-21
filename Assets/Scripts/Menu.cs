using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject shopPanel;
    public Text muteText;
    public Text coinsText;
    public Text highScoreText;

    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioClip buttonClick;

    Animator animator;

    private void Start()
    {
        Time.timeScale = 1;

        animator = GetComponent<Animator>();

        float highScore = PlayerPrefs.GetFloat("HighScore");
        highScoreText.text = highScore.ToString("f0");

        muteText.text = SoundManager.Instance.Muted ? "unmute" : "mute";

        musicSlider.value = SoundManager.Instance.MusicVolume;
        sfxSlider.value = SoundManager.Instance.SfxVolume;
        musicSlider.onValueChanged.AddListener(SoundManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSfxVolume);
    }

    public void Play()
    {
        SoundManager.Instance.PlayUI(buttonClick);
        SoundManager.Instance.RestartMusic();
        SceneManager.LoadScene("MainScene");
    }

    public void Shop()
    {
        SoundManager.Instance.PlayUI(buttonClick);

        animator.SetBool("shop", true);
    }

    public void BackToMenu()
    {
        SoundManager.Instance.PlayUI(buttonClick);

        animator.SetBool("shop", false);
    }

    bool muted;
    public void Mute()
    {
        muted = !muted;
        SoundManager.Instance.MuteMaster(muted);
        SoundManager.Instance.PlayUI(buttonClick);
        muteText.text = muted ? "unmute" : "mute";
    }

    public void Exit()
    {
        SoundManager.Instance.PlayUI(buttonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
