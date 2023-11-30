using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Immortality", menuName = "Powerups/Immortality")]
public class ImmortalitySO : PowerupSO 
{
    public float SpeedBoost => speedBoost;
    [SerializeField] private float speedBoost = 1;
}
