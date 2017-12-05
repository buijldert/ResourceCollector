using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;
using UI;
using Utility;

public class BuyUpgrade : MonoBehaviour
{
    public delegate void UpgradeBoughtAction();
    public static event UpgradeBoughtAction OnUpgradeBought;

    [SerializeField] private Gold _gold;
    [SerializeField] private FuelBar _fuelBar;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private DrillLevel _drillLevel;

    [SerializeField]
    private GameObject _purchaseScreen;

    private float _upgradeOffset = 10f;
    private float _oneOffset = 1f;

    [SerializeField]
    private Text _upgradeText;

    public void UpgradeBuy()
    {
        if (PlayerStats.Gold >= UpgradeData.UpgradeCost)
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["BuySound"], volume: 0.05f);
            UpgradeButton currentUpgradeScript = UpgradeData.UpgradedScript.GetComponent<UpgradeButton>();

            currentUpgradeScript.Bought = true;
            

            if (UpgradeData.UpgradeType == UpgradeTypes.Drill)
            {
                PlayerStats.DrillLevel = UpgradeData.UpgradeLevel;
                PlayerStats.MaxDrillLevel = UpgradeData.UpgradeLevel;
                _drillLevel.ChangeDrillLevelIndication();
                
            }
            else if (UpgradeData.UpgradeType == UpgradeTypes.FuelTank)
            {
                PlayerStats.MaxFuel = (UpgradeData.UpgradeLevel + _oneOffset) * _upgradeOffset * _upgradeOffset;
                _fuelBar.MutateResource(PlayerStats.MaxFuel - PlayerStats.Fuel);
            }
            else if (UpgradeData.UpgradeType == UpgradeTypes.Health)
            {
                PlayerStats.MaxHealth = (UpgradeData.UpgradeLevel + _oneOffset) * _upgradeOffset;
                _healthBar.MutateResource(PlayerStats.MaxHealth - PlayerStats.Health);
            }

            if (OnUpgradeBought != null)
                OnUpgradeBought();

            currentUpgradeScript.SetButtons();

            _gold.MutateGold(-UpgradeData.UpgradeCost);

            _purchaseScreen.SetActive(true);
            _upgradeText.text = "Upgraded " + UpgradeData.UpgradeType + " for $" + UpgradeData.UpgradeCost + ".";
        }
        else
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["BuyErrorSound"], volume: 0.05f);
            _purchaseScreen.SetActive(true);
            _upgradeText.text = "Purchase failed, you don't have enough money.";
        }
    }

}