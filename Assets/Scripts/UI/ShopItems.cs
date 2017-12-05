/*
	ShopItems.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Environment;
using PlayerScripts;
using Utility;

namespace UI
{
    public class ShopItems : MonoBehaviour
    {
        public delegate void WinConditionMetAction();
        public static event WinConditionMetAction OnWinConditionMet;

        //The text components to display the item values on
        [SerializeField] private List<Text> _itemValues = new List<Text>();

        //The text component of the sellbutton.
        [SerializeField] private Text _sellText;

        //The gold script for use when adding gold.
        [SerializeField] private Gold _playerGold;

        //The sellvalue of all items combined.
        private int _sellValue;

        /// <summary>
        /// Loads all items in the inventory to the shop tabs.
        /// </summary>
        public void LoadItems()
        {
            for (int i = 0; i < PlayerInventory.Items.Count; i++)
            {
                string blockType = PlayerInventory.Items[i].BlockType.ToString();
                _itemValues[i].text = blockType + " x " + PlayerInventory.Items[i].Stack;
            }
            CalculateSellValue();
        }

        /// <summary>
        /// Calculates the sell value of all the items in the inventory combined.
        /// </summary>
        private void CalculateSellValue()
        {
            _sellValue = 0;
            for (int i = 0; i < PlayerInventory.Items.Count; i++)
            {
                Block currentBlock = PlayerInventory.Items[i];
                _sellValue += (currentBlock.SellValue * currentBlock.Stack);
            }
            _sellText.text = "Sell all $ " + _sellValue;
        }

        /// <summary>
        /// Sells all items in the inventory for gold.
        /// </summary>
        public void SellAllItems()
        {
            CheckWinCondition();
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["SellSound"], volume:0.05f);
            PlayerInventory.ClearInventory();
            _playerGold.MutateGold(_sellValue);
            for (int i = 0; i < _itemValues.Count; i++)
            {
                _itemValues[i].text = string.Empty;
            }
            CalculateSellValue();
        }

        private void CheckWinCondition()
        {
            for (int i = 0; i < PlayerInventory.Items.Count; i++)
            {
                if(PlayerInventory.Items[i].BlockType == Block.BlockTypes.Artifact)
                {
                    if (OnWinConditionMet != null)
                        OnWinConditionMet();
                }
            }
        }
    }
}