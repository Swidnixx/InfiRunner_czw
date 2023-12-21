using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance
    {
        set => _instance = value;
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("_SoundManager").AddComponent<SoundManager>();
                _instance.sfxSource = _instance.gameObject.AddComponent<AudioSource>();
            }
            return _instance;
        }
    }

    private static SoundManager _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public AudioSource musicSource;
    public AudioSource uiSource;
    public AudioSource sfxSource;

    public AudioMixer mixer;

    int currentMusic;
    public AudioClip[] musics;

    public bool Muted => muted;
    bool muted;

    public float MusicVolume { get; private set; } = 1;
    public float SfxVolume { get; private set; } = 1;

    private void Start()
    {
        RandomMusic();
    }

    private void Update()
    {
        if( musicSource.time > musicSource.clip.length - 0.05f)
        {
            NextMusic();
        }
    }

    public void SetMusicVolume(float vol01)
    {
        float volDb = (1-vol01) * -40;
        mixer.SetFloat("musicVolume", volDb);
        MusicVolume = vol01;
    }

    public void SetSfxVolume(float vol01)
    {
        float volDb = (1 - vol01) * -40;
        mixer.SetFloat("sfxVolume", volDb);
        SfxVolume = vol01;
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayUI(AudioClip clip)
    {
        uiSource.PlayOneShot(clip);
    }

    public void MuteMaster(bool muted)
    {
        this.muted = muted;

        if(muted)
            mixer.SetFloat("masterVolume", -80);
        else
            mixer.SetFloat("masterVolume", 0);

        //musicSource.mute = muted;
        //sfxSource.mute = muted;
        //uiSource.mute = muted;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void RestartMusic()
    {
        RandomMusic();
    }

    private void RandomMusic()
    {
        currentMusic = Random.Range(0, musics.Length);
        musicSource.clip = musics[currentMusic];
        musicSource.Play();
    }
    private void NextMusic()
    {
        currentMusic++;
        currentMusic = currentMusic % musics.Length;
        musicSource.clip = musics[currentMusic];
        musicSource.Play();
    }
}
