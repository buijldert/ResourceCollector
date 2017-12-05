/*
	LoadLevel.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections.Generic;
using System;
using UnityEngine;
using Data;
using Environment;
using PlayerScripts;

namespace Serialization
{
    /// <summary>
    /// The data that will be serialized to a savefile.
    /// </summary>
    [Serializable]
    public class SerializationData
    {
        public int AmountOfBombs;
        public int DrillLevel;
        public int MaxDrillLevel;
        public int Gold;
        public float Fuel;
        public float MaxFuel;
        public float Health;
        public float MaxHealth;

        public int[,] TopLevel;
        public int[,] MidLevel;
        public int[,] BottomLevel;

        public List<SaveableBlock> InventoryBlocks;
    }

    /// <summary>
    /// A saveable version of the Block.cs class.
    /// </summary>
    [Serializable]
    public class SaveableBlock
    {
        public Block.BlockTypes BlockType;
        public int Stack;
        public int SellValue;
    }

    /// <summary>
    /// Saves the level and playerdata.
    /// </summary>
    public class SaveLevel : MonoBehaviour
    {
        [SerializeField]
        private BigLevelGenerator _bigLevelGenerator;

        public void SaveTheLevel()
        {
            List<SaveableBlock> saveableBlocks = new List<SaveableBlock>();
            foreach (Block block in PlayerInventory.Items)
            {
                SaveableBlock sb = new SaveableBlock
                {
                    BlockType = block.BlockType,
                    Stack = block.Stack,
                    SellValue = block.SellValue
                };
                saveableBlocks.Add(sb);
            }
            SerializationData sData = new SerializationData
            {
                DrillLevel = PlayerStats.DrillLevel,
                MaxDrillLevel = PlayerStats.MaxDrillLevel,
                Gold = PlayerStats.Gold,
                AmountOfBombs = PlayerStats.AmountOfBombs,
                Fuel = PlayerStats.Fuel,
                MaxFuel = PlayerStats.MaxFuel,
                Health = PlayerStats.Health,
                MaxHealth = PlayerStats.MaxHealth,

                TopLevel = _bigLevelGenerator.TopLevel,
                MidLevel = _bigLevelGenerator.MidLevel,
                BottomLevel = _bigLevelGenerator.BottomLevel,

                InventoryBlocks = saveableBlocks
            };

            Serializer.Save(InlineStrings.SAVEFILENAME, sData);
        }
    }
}