using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupPanelUI : MonoBehaviour
{
    [SerializeField] private Traveller magnet, battery;
    private Vector2 startPos => new Vector2(210, 0);
    private Vector2 endPos => new Vector2(-210, 0);

    Coroutine magnetActiveCoroutine, batteryActiveCoroutine;

    private void Start()
    {
        magnet.rectT.anchoredPosition = battery.rectT.anchoredPosition = startPos;
        Hide(magnet);
        Hide(battery);
    }

    public void ActivateBattery(float t)
    {
        if (batteryActiveCoroutine != null)
            StopCoroutine(batteryActiveCoroutine);
        ShowUp(battery);
        batteryActiveCoroutine = StartCoroutine(LerpPosition(t, battery));
    }

    public void ActivateMagnet(float t)
    {
        if (magnetActiveCoroutine != null)
            StopCoroutine(magnetActiveCoroutine);
        ShowUp(magnet);
        magnetActiveCoroutine = StartCoroutine(LerpPosition(t, magnet));
    }

    void ShowUp(Traveller t)
    {
        Hide(t);
        t.rectT.anchoredPosition = startPos;
        t.rectT.gameObject.SetActive(true);
    }

    void Hide(Traveller t)
    {
        t.rectT.gameObject.SetActive(false);
    }

    private IEnumerator LerpPosition(float t_sec, Traveller traveller)
    {
        for (float t = 0; t < t_sec; t += Time.deltaTime)
        {
            Vector2 nextPos = Vector2.Lerp(startPos, endPos, t / t_sec);
            traveller.rectT.anchoredPosition = nextPos;
            traveller.text.text = (t_sec - (int)t) + " s";
            yield return null;
        }
        Hide(traveller);
    }
}

[Serializable]
public class Traveller
{
    public RectTransform rectT;
    public Text text;

}
