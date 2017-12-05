/*
	BlockBreak.cs
	Created 10/6/2017 10:27:39 AM
	Project Resource Collector by Base Games
*/
using Extensions;
using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Environment
{

	public class BlockBreak : MonoBehaviour 
	{
        [SerializeField]
        private List<BrokenBlock> _brokenBlocks;
        private List<Sprite> _breakSprites;
        private List<GameObject> _breakGameObjects = new List<GameObject>();

        private Dictionary<Block.BlockTypes, List<Sprite>> _breakSpritesDictionary = new Dictionary<Block.BlockTypes, List<Sprite>>();

        [SerializeField]
        private GameObject _crumblePrefab;

        private int _currentSprite = 0;
        private int _squareOffset = 4;

        private float _posX, _posY;

        private void OnEnable()
        {
            Harvester.OnHarvest += StartBreak;

            foreach(BrokenBlock bb in _brokenBlocks)
            {
                _breakSpritesDictionary.Add(bb.BrokenBlockType, bb.BrokenSprites);
            }
        }

        private void OnDisable()
        {
            Harvester.OnHarvest -= StartBreak;
        }

        public void StartBreak(Transform objectPos)
        {
            _breakSprites = _breakSpritesDictionary[objectPos.GetComponent<Block>().BlockType];
            _breakGameObjects.Clear();
            _breakGameObjects = new List<GameObject>();
            _currentSprite = 0;
            _posX = 0;
            _posY = 0;
            

            int blockSize = _breakSprites.Count / _squareOffset;

            GameObject crumbleClone;

            for (int y = 0; y < blockSize; y++)
            {
                for (int x = 0; x < blockSize; x++)
                {
                    crumbleClone = ObjectPool.Instance.GetObjectForType(_crumblePrefab.name, false);
                    crumbleClone.transform.position = new Vector2((objectPos.position.x + _posX) - .375f, (objectPos.position.y + _posY) + .375f);
                    crumbleClone.GetComponent<SpriteRenderer>().sprite = _breakSprites[_currentSprite];
                    _breakGameObjects.Add(crumbleClone);
                    _currentSprite += 1;
                    _posX += 1f / _squareOffset;
                }
                _posX = 0;
                _posY -= 1f / _squareOffset;
            }
            _breakGameObjects.ShuffleList();
            StartCoroutine(BreakCoroutine());
        }

        private IEnumerator BreakCoroutine()
        {
            for (int i = 0; i < _breakGameObjects.Count; i++)
            {
                _breakGameObjects[i].GetComponent<Rigidbody2D>().gravityScale = 1f;
                yield return new WaitForSeconds(.01f);
            }
        }
    }
}