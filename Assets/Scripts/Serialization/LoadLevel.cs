/*
	LoadLevel.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections.Generic;
using UnityEngine;
using Data;
using Environment;
using PlayerScripts;
using Utility;
using UI;
using System.Collections;

namespace Serialization
{
    public class LoadLevel : MonoBehaviour
    {
        [SerializeField]
        private BigLevelGenerator _bigLevelGenerator;

        [SerializeField] private DrillLevel _drillLevelScript;

        //The drill level of the player to be set in the editor.
        [SerializeField]
        private int _drillLevel = 0;
        //The gold of the player to be set in the editor.
        [SerializeField]
        private int _gold = 60;

        [SerializeField]private int _amountOfBombs = 0;

        //The fuel of the player to be set in the editor.
        [SerializeField]
        private float _fuel = 100f;
        //The health of the player to be set in the editor.
        [SerializeField]
        private float _health = 10f;

        //Whether a save file exists.
        public static bool SaveFileExists;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void Awake()
        {
            LoadTheLevel();

            StartCoroutine(StartDelay());
        }

        private IEnumerator StartDelay()
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(SoundSwitchFade.instance.FadeIn(SoundsDatabase.AudioClips["BackgroundMusic"]));
        }

        /// <summary>
        /// Loads the level if a savefile exists. If it doesn't exist start a new game.
        /// </summary>
        private void LoadTheLevel()
        {
            SerializationData sData = Serializer.Load<SerializationData>(InlineStrings.SAVEFILENAME);

            if (sData != null)
            {
                PlayerStats.DrillLevel = sData.DrillLevel;
                PlayerStats.MaxDrillLevel = sData.DrillLevel;
                PlayerStats.Gold = sData.Gold;
                PlayerStats.AmountOfBombs = sData.AmountOfBombs;
                PlayerStats.Fuel = sData.Fuel;
                PlayerStats.MaxFuel = sData.MaxFuel;
                PlayerStats.Health = sData.Health;
                PlayerStats.MaxHealth = sData.MaxHealth;

                _bigLevelGenerator.TopLevel = sData.TopLevel;
                _bigLevelGenerator.MidLevel = sData.MidLevel;
                _bigLevelGenerator.BottomLevel = sData.BottomLevel;

                List<Block> inventoryBlocks = new List<Block>();
                foreach (SaveableBlock sb in sData.InventoryBlocks)
                {
                    Block block = new Block
                    {
                        BlockType = sb.BlockType,
                        Stack = sb.Stack,
                        SellValue = sb.SellValue
                    };
                    inventoryBlocks.Add(block);
                }

                PlayerInventory.Items = inventoryBlocks;

                SaveFileExists = true;
            }
            else
            {
                SaveFileExists = false;
                PlayerStats.DrillLevel = _drillLevel;
                PlayerStats.MaxDrillLevel = _drillLevel;
                PlayerStats.Gold = _gold;
                PlayerStats.AmountOfBombs = _amountOfBombs;
                PlayerStats.MaxFuel = _fuel;
                PlayerStats.Fuel = _fuel;
                PlayerStats.Health = _health;
                PlayerStats.MaxHealth = _health;
            }

            _drillLevelScript.ChangeDrillLevelIndication();
        }
    }
}