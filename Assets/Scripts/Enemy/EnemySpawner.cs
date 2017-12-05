/*
	EnemySpawner.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        
        [SerializeField] private float _spawnChance = 7f;
        private float _lastDrop = 101;

        private void OnEnable()
        {
            Harvester.OnHarvest += SpawnEnemy;
        }

        private void OnDisable()
        {
            Harvester.OnHarvest -= SpawnEnemy;
        }

        private void SpawnEnemy(Transform brokenBlockTransform)
        {
            float drop = Random.Range(0, 100f);

            if (drop < _spawnChance)
            {
                if(!(_lastDrop < _spawnChance))
                {
                    GameObject enemyClone = ObjectPool.Instance.GetObjectForType(_enemyPrefab.name, false);
                    enemyClone.transform.position = brokenBlockTransform.position;
                }
            }

            _lastDrop = drop;
        }
    }
}