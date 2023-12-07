using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PowerupManager powerupManager;

    public Text coinsText;
    int coins;

    public Text immortalityInfoText;
    public Button immortalityUpgradeButton;
    public Text magnetInfoText;
    public Button magnetUpgradeButton;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        coinsText.text = coins.ToString();

        DisplayImmortalityInfo();
    }

    void DisplayImmortalityInfo()
    {
        string info = "Lvl " + powerupManager.Immortality.level + "\n";

        if( powerupManager.Immortality.upgraded != null )
        {
            info += "$" + powerupManager.Immortality.upgradeCost + " to upgrade";
        }
        else
        {
            info += "Max level!";
            immortalityUpgradeButton.interactable = false;
        }

        immortalityInfoText.text = info;
    }

    public void UpgradeImmortality()
    {
        if( coins >= powerupManager.Immortality.upgradeCost )
        {
            coins -= powerupManager.Immortality.upgradeCost;
            PlayerPrefs.SetInt("Coins", coins);
            coinsText.text = coins.ToString();

            powerupManager.Immortality = powerupManager.Immortality.upgraded as ImmortalitySO;
            DisplayImmortalityInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
