using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonUI : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
