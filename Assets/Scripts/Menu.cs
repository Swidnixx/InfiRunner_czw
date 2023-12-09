using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject shopPanel;

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Shop()
    {
        menuPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    bool muted;
    public void Mute()
    {
        muted = !muted;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
