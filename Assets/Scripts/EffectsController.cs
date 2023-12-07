using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsController : MonoBehaviour
{
    public GameObject fire;
    public Animator dirtOverlay;

    public Image scorePanel;
    public Image highScoreFill;
    public Material burnUImaterial;
    public Material burnUImaterialFill;

    float score => GameManager.Instance.score;
    float highScore => GameManager.Instance.highScore;

    private void Start()
    {
        GameManager.Instance.OnHighScoreReached += HighScoreRun;

        highScoreFill.fillAmount = 0;

        scorePanel.material = null;
        highScoreFill.material = null;
    }

    private void Update()
    {
        highScoreFill.fillAmount = score / highScore;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnHighScoreReached -= HighScoreRun;
    }

    void HighScoreRun()
    {
        highScoreFill.material = burnUImaterialFill;
        scorePanel.material = burnUImaterial;
        scorePanel.color = Color.white;
    }

    public void BatteryEnable(bool enabled)
    {
        dirtOverlay.SetBool("enabled", enabled);
        fire.SetActive(enabled);
    }
}
