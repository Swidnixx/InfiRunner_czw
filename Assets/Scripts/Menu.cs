using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject shopPanel;

    public AudioClip buttonClick;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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

        //menuPanel.SetActive(false);
        //shopPanel.SetActive(true);
        animator.SetBool("shop", true);
    }

    public void BackToMenu()
    {
        SoundManager.Instance.PlayUI(buttonClick);

        //menuPanel.SetActive(true);
        //shopPanel.SetActive(false);
        animator.SetBool("shop", false);
    }

    bool muted;
    public void Mute()
    {
        muted = !muted;
        SoundManager.Instance.MuteMaster(muted);
        SoundManager.Instance.PlayUI(buttonClick);
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
