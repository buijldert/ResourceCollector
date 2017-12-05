/*
	UpgradeButton.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.UI;
using Data;
using Utility;

namespace UI
{
    public class UpgradeButton : MonoBehaviour
    {
        //An event to be sent out when the upgradebutton is pressed.
        public delegate void ButtonPressedAction();
        public static event ButtonPressedAction OnButtonPressed;

        //The equipbutton gameobject.
        [SerializeField] private GameObject _equipButton;
        //The buy button gameobject.
        [SerializeField] private GameObject _buyButton;

        //The that displays the cost of the upgrade.
        [SerializeField] private Text _costText, _priceText;

        private Outline _outline;

        //The type of upgrade.
        [SerializeField] private UpgradeTypes _upgradeType;

        //A check if the upgrade has been bought.
        [SerializeField] private bool _bought;
        public bool Bought
        {
            get { return _bought; }
            set { _bought = value; }
        }

        //The level of the upgrade.
        [SerializeField]
        private int _upgradeLevel;
        //The cost of the upgrade.
        [SerializeField]
        private int _upgradeCost;

        private string _boughtString = "Owned";
        private string _equippedString = "Equipped";

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            EquipDrill.OnDrillChanged += SetButtons;
            BuyUpgrade.OnUpgradeBought += SetButtons;
            OnButtonPressed += DisableHighlight;

            switch(_upgradeType)
            {
                case UpgradeTypes.Drill:
                    if (PlayerStats.DrillLevel == _upgradeLevel)
                    {
                        _bought = true;
                        _priceText.text = _equippedString;
                    }
                    else if (PlayerStats.MaxDrillLevel > _upgradeLevel)
                    {
                        _bought = true;
                        _priceText.text = _boughtString;
                    }
                    break;
                case UpgradeTypes.FuelTank:
                    if ((PlayerStats.MaxFuel / 100f) - 1 == _upgradeLevel)
                    {
                        _bought = true;
                        _priceText.text = _equippedString;
                    }
                    else if ((PlayerStats.MaxFuel / 100f) - 1 > _upgradeLevel)
                    {
                        _bought = true;
                        _priceText.text = _boughtString;
                    }
                    break;
                case UpgradeTypes.Health:
                    if((PlayerStats.MaxHealth / 10) - 1 == _upgradeLevel)
                    {
                        _bought = true;
                        _priceText.text = _equippedString;
                    }
                    else if ((PlayerStats.MaxHealth / 10) - 1 > _upgradeLevel)
                    {
                        _bought = true;
                        _priceText.text = _boughtString;
                    }
                    break;
            }
        }

        private void Start()
        {
            _outline = GetComponent<Outline>();
        }

        /// <summary>
        /// Use this for de-initialization.
        /// </summary>
        private void OnDisable()
        {
            EquipDrill.OnDrillChanged -= SetButtons;
            BuyUpgrade.OnUpgradeBought -= SetButtons;
            OnButtonPressed -= DisableHighlight;
        }

        /// <summary>
        /// Changes the current upgrade.
        /// </summary>
        public void ChangeUpgrade()
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["GeneralClickSound"], volume: 0.05f);
            if (OnButtonPressed != null)
            {
                OnButtonPressed();
            }

            _outline.enabled = true;

            UpgradeData.UpgradeType = _upgradeType;
            UpgradeData.UpgradeLevel = _upgradeLevel;
            UpgradeData.UpgradeCost = _upgradeCost;
            UpgradeData.UpgradedScript = this;

            SetButtons();
        }

        /// <summary>
        /// Disables the highlight of the button when another is pressed.
        /// </summary>
        private void DisableHighlight()
        {
            _outline.enabled = false;
        }

        /// <summary>
        /// Sets the upgradebuttons.
        /// </summary>
        public void SetButtons()
        {
            if (!_bought)
            {
                _equipButton.SetActive(false);
                _buyButton.SetActive(true);
                _costText.text = "Buy " + _upgradeType + " upgrade for $" + _upgradeCost;
                _priceText.text = "$" + _upgradeCost;
            }
            else if (_bought && _upgradeType == UpgradeTypes.Drill)
            {
                _buyButton.SetActive(false);
                
                if(_upgradeLevel == PlayerStats.DrillLevel)
                {
                    _equipButton.SetActive(false);
                    _priceText.text = _equippedString;
                }
                else
                {
                    _equipButton.SetActive(true);
                    _priceText.text = _boughtString;
                }
            }
            else if (_bought && _upgradeType != UpgradeTypes.Drill)
            {
                _equipButton.SetActive(false);
                _buyButton.SetActive(false);

                if ((_upgradeType == UpgradeTypes.FuelTank && (PlayerStats.MaxFuel / 100f) - 1 == _upgradeLevel) || (_upgradeType == UpgradeTypes.Health && (PlayerStats.MaxHealth / 10) -1 == _upgradeLevel))
                    _priceText.text = _equippedString;
                else
                    _priceText.text = _boughtString;
            }
        }
    }
}