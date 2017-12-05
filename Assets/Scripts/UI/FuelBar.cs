/*
	FuelBar.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.UI;
using Data;
using PlayerScripts;
using Utility;
using DG.Tweening;
using System.Collections;

namespace UI
{
    public class FuelBar : MonoBehaviour
    {
        public delegate void FuelDepeletedAction();
        public static event FuelDepeletedAction OnFuelDepeleted;

        //The resource bar image.
        [SerializeField] private Image _resourceBar;
        [SerializeField] private Text _resourceText;

        [SerializeField] private GameObject _flashingScreen;

        [SerializeField] private Text _lowFuelText;

        private ColorLerp _lerpFuelText;

        private void Start()
        {
            _resourceBar.fillAmount = PlayerStats.Fuel / PlayerStats.MaxFuel;
            _lerpFuelText = _lowFuelText.GetComponent<ColorLerp>();
        }

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            PlayerMovement.OnMovingAction += MutateResource;
            Harvester.OnHarvestStart += MutateResource;
        }

        /// <summary>
        /// Use this for de - initialization.
        /// </summary>
        private void OnDisable()
        {
            PlayerMovement.OnMovingAction -= MutateResource;
            Harvester.OnHarvestStart -= MutateResource;
        }
        
        /// <summary>
        /// Mutates the resource by the parameters amount ex: MutateResource(20) = _resource + 20.
        /// MutateResource(-20) = _resource - 20.
        /// </summary>
        /// <param name="resourceMutation">The amount of resources with which the resource will change (-/+).</param>
        public void MutateResource(float resourceMutation)
        {
            PlayerStats.Fuel += resourceMutation;
            if (PlayerStats.Fuel > PlayerStats.MaxFuel)
                PlayerStats.Fuel = PlayerStats.MaxFuel;

            _resourceBar.DOFillAmount(PlayerStats.Fuel / PlayerStats.MaxFuel, 0.5f);
            _resourceText.text = (int)PlayerStats.Fuel / 10f + "/" + (int)(PlayerStats.MaxFuel / 10f);
            float halfFuel = PlayerStats.MaxFuel / 2;
            float quarterFuel = halfFuel / 2;

            if(PlayerStats.Fuel >= halfFuel)
            {
                _lerpFuelText.ControlLerp(false);
                _flashingScreen.SetActive(false);
            } else if (PlayerStats.Fuel < halfFuel && PlayerStats.Fuel >= quarterFuel)
            {
                _lerpFuelText.ControlLerp(true);
                _lowFuelText.text = "Low Fuel!";
            } else if (PlayerStats.Fuel < quarterFuel && PlayerStats.Fuel > 0)
            {
                _flashingScreen.SetActive(true);
                _lowFuelText.text = "Critical Fuel! RETURN TO THE SHOP IMMEDIATELY!";

            } else if (PlayerStats.Fuel <= 0)
            {
                PlayerStats.Fuel = 0;
                _resourceText.text = (int)PlayerStats.Fuel / 10f + "/" + (int)(PlayerStats.MaxFuel / 10f);
                _lerpFuelText.ControlLerp(false);
                _flashingScreen.SetActive(false);

                if (OnFuelDepeleted != null)
                    OnFuelDepeleted();

                GameState.CGameState = CurrentGameState.Paused;
            }
        }
    }
}