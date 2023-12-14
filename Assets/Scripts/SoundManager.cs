using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool muted;
    public void ToggleMuted()
    {
        muted = !muted;
        audioSource.mute = muted;
    }

    public AudioClip jumpSfx;
    public void PlayJumpSfx()
    {
        audioSource.PlayOneShot(jumpSfx);
    }

    public AudioClip buttonClick, coinCollect;
    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
    public void PlayCoinCollect()
    {
        audioSource.PlayOneShot(coinCollect);
    }
}
