/*
	Block.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using Utility;

namespace Environment
{
    /// <summary>
    /// A class containing all data for the blocks that can be mined.
    /// </summary>
    [System.Serializable]
    public class Block : MonoBehaviour
    {
        public enum BlockTypes { Dirt, Stone, Bronze, Iron, Gold, Diamond, Amethyst, Opal, Artifact }
        public BlockTypes BlockType;
        public enum LevelTypes { TopLevel, MidLevel, BottomLevel }
        public LevelTypes LevelType;

        public float Probability;

        public int DrillRequirement;
        public int SellValue;
        public int Stack = 0;

        [System.NonSerialized]
        public int2 ArrayPosition;
    }
}