/*
	BigLevelGenerator.cs
	Created 9/29/2017 3:07:01 PM
	Project Resource Collector by Base Games
*/
using Serialization;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Environment
{
	public class BigLevelGenerator : MonoBehaviour 
	{
        //The prefabs of the resources.
        [SerializeField]
        private List<GameObject> _midLevelPrefabs;
        //The list of probabilities per resource.
        private List<float> _midLevelProbabilities = new List<float>();

        [SerializeField]
        private List<GameObject> _topLevelPrefabs;
        private List<float> _topLevelProbabilities = new List<float>() { 0f, 0f, 10f, 20f, 20f, 50f };

        [SerializeField]
        private GameObject _diamondPrefab, _artifactPrefab, _emptyPrefab, _opalPrefab;
        
        private int _positionX, _positionY;

        private int _tilePrefabNumber;

        public int[,] TopLevel = new int[10, 50]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9},
            {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9},
        };
        //The bottom level of the game
        public int[,] MidLevel = new int[50, 100];
        public int[,] BottomLevel = new int[2, 50]
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

        /// <summary>
        /// Randomly generates the level by using percentages per block.
        /// </summary>
        void Start()
        {
            BuildTopLevel();
            BuildMidLevel();
            BuildBottomLevel();
        }

        /// <summary>
        /// Builds the top level based on its two dimensional array.
        /// </summary>
        private void BuildTopLevel()
        {
            GameObject tileClone;
            Block blockClone;

            for (int x = 0; x < TopLevel.GetLength(0); x++)
            {
                for (int y = 0; y < TopLevel.GetLength(1); y++)
                {
                    switch (TopLevel[x, y])
                    {
                        case 0:
                            _tilePrefabNumber = 0;
                            break;
                        case 1:
                            _tilePrefabNumber = 1;
                            //spawn
                            break;
                        case 2:
                            _tilePrefabNumber = 2;
                            //spawn
                            break;
                        case 3:
                            _tilePrefabNumber = 3;
                            break;
                        case 4:
                            _tilePrefabNumber = 4;
                            break;
                        case 5:
                            _tilePrefabNumber = 5;
                            break;
                        case 9:
                            _tilePrefabNumber = _tilePrefabNumber.CalculateProbability(_topLevelProbabilities);
                            break;
                        case 10:
                            _tilePrefabNumber = 0;
                            break;
                    }
                    tileClone = ObjectPool.Instance.GetObjectForType(_topLevelPrefabs[_tilePrefabNumber].name, false);

                    PoolableBlocks.PoolableSquares.Add(tileClone);

                    TopLevel[x, y] = _tilePrefabNumber;

                    tileClone.transform.position = new Vector2(transform.position.x - _positionX, transform.position.y - _positionY);
                    tileClone.transform.parent = transform;
                    if (_tilePrefabNumber != 0)
                    {
                        blockClone = tileClone.GetComponent<Block>();
                        blockClone.ArrayPosition = new int2(x, y);
                        blockClone.LevelType = Block.LevelTypes.TopLevel;
                    }
                    _positionX += 1;
                }
                _positionX = 0;
                _positionY += 1;
            }
        }

        /// <summary>
        /// Gemerates the midlevel if there is no save file.
        /// </summary>
        private void BuildMidLevel()
        {
            for (int i = 0; i < _midLevelPrefabs.Count; i++)
            {
                _midLevelProbabilities.Add(_midLevelPrefabs[i].GetComponent<Block>().Probability);
            }

            GameObject terrainClone;
            Block blockClone;

            int randomResource = 0;

            for (int y = 0; y < MidLevel.GetLength(1); y++)
            {
                for (int x = 0; x < MidLevel.GetLength(0); x++)
                {
                    if (MidLevel[x, y] != 10)
                    {
                        if (LoadLevel.SaveFileExists)
                        {
                            terrainClone = ObjectPool.Instance.GetObjectForType(_midLevelPrefabs[MidLevel[x, y]].name, false);
                        }
                        else
                        {
                            randomResource = randomResource.CalculateProbability(_midLevelProbabilities);//CalculateProbability(_midLevelProbabilities);

                            while(_midLevelPrefabs[randomResource].name == _opalPrefab.name && y < 20 || _midLevelPrefabs[randomResource].name == _diamondPrefab.name && y < 40)
                            {
                                randomResource = randomResource.CalculateProbability(_midLevelProbabilities);
                            }

                            MidLevel[x, y] = randomResource;
                            terrainClone = ObjectPool.Instance.GetObjectForType(_midLevelPrefabs[randomResource].name, false);
                        }

                        terrainClone.transform.position = new Vector2(transform.position.x - _positionX, transform.position.y - _positionY);
                        terrainClone.transform.parent = transform;
                        blockClone = terrainClone.GetComponent<Block>();
                        blockClone.ArrayPosition = new int2(y, x);
                        blockClone.LevelType = Block.LevelTypes.MidLevel;
                    }
                    _positionX += 1;
                }
                _positionX = 0;
                _positionY += 1;
            }
        }

        /// <summary>
        /// Builds the bottom level depending on its two dimensional array.
        /// </summary>
        private void BuildBottomLevel()
        {
            if (!LoadLevel.SaveFileExists)
            {
                int artifactX = Random.Range(0, 50);
                BottomLevel[1, artifactX] = 100;
            }

            GameObject tileClone;
            Block blockClone;

            for (int y = 0; y < BottomLevel.GetLength(0); y++)
            {
                for (int x = 0; x < BottomLevel.GetLength(1); x++)
                {
                    switch (BottomLevel[y, x])
                    {
                        case 10:
                            tileClone = ObjectPool.Instance.GetObjectForType(_emptyPrefab.name, false);
                            break;
                        case 100:
                            tileClone = ObjectPool.Instance.GetObjectForType(_artifactPrefab.name, true);
                            break;
                        default:
                            tileClone = tileClone = ObjectPool.Instance.GetObjectForType(_diamondPrefab.name, false);
                            break;
                    }

                    tileClone.transform.position = new Vector2(transform.position.x - _positionX, transform.position.y - _positionY);
                    tileClone.transform.parent = transform;

                    PoolableBlocks.PoolableSquares.Add(tileClone);

                    blockClone = tileClone.GetComponent<Block>();
                    blockClone.ArrayPosition = new int2(y, x);
                    blockClone.LevelType = Block.LevelTypes.BottomLevel;

                    _positionX += 1;
                }
                _positionX = 0;
                _positionY += 1;
            }
        }
    }
}