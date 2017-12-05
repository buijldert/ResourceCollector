/*
	RepairHealth.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.UI;
using Data;
using Utility;

namespace UI
{
    public class RepairHealth : MonoBehaviour
    {
        //The healthbar script for use when adding health.
        [SerializeField]
        private HealthBar _healthBar;

        //The gold script for use when removing gold.
        [SerializeField]
        private Gold _gold;

        //The text component that the cost of repair will be displayed on.
        [SerializeField]
        private Text _costText;

        //The cost of each healthpoint.
        private int _costPerHealthPoint = 2;
        //The final cost of repair.
        private int _finalCost;
        //The final amount of health to be added.
        private int _finalAmountOfHealth;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            CalculateRepairCost();
        }

        /// <summary>
        /// Repair the health of the player.
        /// </summary>
        public void Repair()
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["BuySound"], volume: 0.05f);
            _gold.MutateGold(-_finalCost);
            _healthBar.MutateResource(_finalAmountOfHealth);
            CalculateRepairCost();
        }

        /// <summary>
        /// Calculates the cost and amount of health to be added for repairing.
        /// </summary>
        private void CalculateRepairCost()
        {
            int health = Mathf.RoundToInt(PlayerStats.Health);
            int maxFuel = Mathf.RoundToInt(PlayerStats.MaxHealth);
            int healthToBuy = maxFuel - health;
            int cost = healthToBuy * _costPerHealthPoint;

            if (PlayerStats.Gold < cost)
            {
                //Makes sure the player can always buy fuel(unless they have less that 3 gold).
                while (PlayerStats.Gold < cost)
                {
                    healthToBuy -= 1;
                    cost -= _costPerHealthPoint;
                }
            }

            _finalCost = cost;
            _finalAmountOfHealth = healthToBuy;
            _costText.text = _finalCost + "$ for: " + _finalAmountOfHealth + "HP";
        }
    }
}