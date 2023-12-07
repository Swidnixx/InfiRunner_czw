using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class PowerupManager : ScriptableObject
{
    [SerializeField] private ImmortalitySO immortality;
    [SerializeField] private MagnetSO magnet;

    public ImmortalitySO Immortality 
    { 
        get { return immortality; } 
        set 
        { 
            immortality = value;
            PlayerPrefs.SetString("ImmortalityLevel", immortality.name);
            Debug.Log(immortality.name + " was saved");
        } 
    }
    public MagnetSO Magnet
    {
        get { return magnet; }
        set { magnet = value; }
    }

    public void Init()
    {
        ImmortalitySO tmp =
            Resources.Load<ImmortalitySO>(PlayerPrefs.GetString("ImmortalityLevel"));

        if(tmp != null)
        {
            immortality = tmp;
            Debug.Log("Immortality " + tmp.name + " was loaded");
        }
    }
}
