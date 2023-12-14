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
        set 
        { 
            magnet = value;
            PlayerPrefs.SetString("MagnetLevel", magnet.name);
            Debug.Log(magnet.name + " was saved");
        }
    }

    public void Init()
    {
        //Immortality
        ImmortalitySO tmp =
            Resources.Load<ImmortalitySO>(PlayerPrefs.GetString("ImmortalityLevel"));

        if(tmp != null)
        {
            immortality = tmp;
            Debug.Log("Immortality: " + tmp.name + " was loaded");
        }
        else
        {
            Debug.Log("Default immortality: " + immortality.name);
        }

        //Magnet
        MagnetSO tmp2 =
            Resources.Load<MagnetSO>(PlayerPrefs.GetString("MagnetLevel"));

        if (tmp2 != null)
        {
            magnet = tmp2;
            Debug.Log("Magnet: " + tmp2.name + " was loaded");
        }
        else
        {
            Debug.Log("Default magnet: " + magnet.name);
        }
    }
}
