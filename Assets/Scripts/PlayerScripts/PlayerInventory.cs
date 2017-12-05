/*
	PlayerInventory.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections.Generic;
using UnityEngine;
using Environment;

namespace PlayerScripts
{
    public class PlayerInventory : MonoBehaviour
    {

        public static List<Block> Items = new List<Block>();

        public static void SortList()
        {
            Items.Sort(delegate (Block i1, Block i2) { return i1.BlockType.CompareTo(i2.BlockType); });
        }

        public static bool CheckForDupes(Block.BlockTypes block)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].BlockType == block)
                {
                    return true;
                }
            }
            return false;
        }

        public static Block GetCurrentMinedBlock(Block block)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (block.BlockType == Items[i].BlockType)
                {
                    return Items[i];
                }
            }
            return null;
        }

        public static void ClearInventory()
        {
            ClearBlockStacks();
            Items.Clear();
        }

        private static void ClearBlockStacks()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Stack = 0;
            }
        }
    }
}