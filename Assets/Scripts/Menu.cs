using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel, shopPanel;
    public PowerupManager powerupManager;

    private void Awake()
    {
        powerupManager.Init();
    }

    private void Start()
    {
        BackToMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToShop()
    {
        menuPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        shopPanel.SetActive(false);
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
