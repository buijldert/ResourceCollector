/*
	FuelRefill.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.UI;
using Data;
using Utility;

namespace UI
{
    public class FuelRefill : MonoBehaviour
    {
        //The fuelbar class for use when refilling the fuel.
        [SerializeField]
        private FuelBar _fuelBar;

        //The gold class for use when changing the amount of gold the player has.
        [SerializeField]
        private Gold _gold;

        //The text component that the cost of refueling will be displayed on.
        [SerializeField]
        private Text _costText;

        //The cost per liter of fuel.
        private int costPerLiter = 3;
        //The final cost of refueling.
        private int _finalCost;

        //The fuel resource offset.
        private float _resourceOffset = 10;
        //The final amount of fuel to be refueled.
        private float _finalAmountOfFuel;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            CalculateFuelCost();
        }

        /// <summary>
        /// Refills the fuel based on the calculated amount of fuel and cost.
        /// </summary>
        public void Refill()
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["BuySound"], volume: 0.05f);
            _gold.MutateGold(-_finalCost);
            _fuelBar.MutateResource(_finalAmountOfFuel * _resourceOffset);
            CalculateFuelCost();
        }

        /// <summary>
        /// Calculates the amount of fuel that can be refueled.
        /// </summary>
        private void CalculateFuelCost()
        {
            int fuel = Mathf.RoundToInt(PlayerStats.Fuel / _resourceOffset);
            int maxFuel = Mathf.RoundToInt(PlayerStats.MaxFuel / _resourceOffset);
            int fuelToBuy = maxFuel - fuel;
            int cost = fuelToBuy * costPerLiter;

            if (PlayerStats.Gold < cost)
            {
                //Makes sure the player can always buy fuel(unless they have less that 3 gold).
                while (PlayerStats.Gold < cost)
                {
                    fuelToBuy -= 1;
                    cost -= costPerLiter;
                }
            }
            _finalCost = cost;
            _finalAmountOfFuel = fuelToBuy;
            _costText.text = "$" + _finalCost + " for: " + _finalAmountOfFuel + "L";
        }
    }
}