/*
	BrokenBlock.cs
	Created 10/6/2017 11:35:01 AM
	Project Resource Collector by Base Games
*/
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [System.Serializable]
	public class BrokenBlock 
	{
        public Block.BlockTypes BrokenBlockType;
        public List<Sprite> BrokenSprites;
	}
}