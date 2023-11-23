using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magnet", menuName = "Powerups/Magnet")]
public class MagnetSO : PowerupSO
{
    public float MagnetDistance => magnetDistance;
    [SerializeField] float magnetDistance = 5;
}
