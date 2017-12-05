/*
	PoolableBlocks.cs
	Created 9/29/2017 3:45:50 PM
	Project Resource Collector by Base Games
*/
using Data;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Environment
{
	public class PoolableBlocks : MonoBehaviour 
	{
		public static List<GameObject> PoolableSquares = new List<GameObject>();

        private void Awake()
        {
            PoolableSquares = new List<GameObject>();
        }

        public void PoolBlocks()
        {
            for (int i = 0; i < PoolableSquares.Count; i++)
            {
                ObjectPool.Instance.PoolObject(PoolableSquares[i]);
            }
            PoolableSquares.Clear();

            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag(InlineStrings.ENEMYTAG))
                ObjectPool.Instance.PoolObject(enemy);

            foreach (GameObject bg in GameObject.FindGameObjectsWithTag(InlineStrings.BACKGROUNDTAG))
                ObjectPool.Instance.PoolObject(bg);
        }
	}
}