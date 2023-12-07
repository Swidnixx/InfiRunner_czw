using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public GameObject fire;
    public Animator dirtOverlay;

    public void BatteryEnable(bool enabled)
    {
        dirtOverlay.SetBool("enabled", enabled);
        fire.SetActive(enabled);
    }
}
