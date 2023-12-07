using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupSO : ScriptableObject
{
    public bool IsActive { get; set; }
    public float Duration => duration;
    [SerializeField] float duration = 10;

    public int level = 1;
    public int upgradeCost = 100;
    public PowerupSO upgraded;
}
