/*
	BackgroundPlacer.cs
	Created 10/16/2017 10:24:18 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using Utility;

namespace Environment
{
	public class BackgroundPlacer : MonoBehaviour 
	{
        [SerializeField] private GameObject _underBackgroundPrefab;

        [SerializeField] private Vector2 _startPos;

        private float _width, _height;

        private void Start()
        {
            _width = _underBackgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
            _height = _underBackgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.y;

            GameObject bgClone;

            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    bgClone = ObjectPool.Instance.GetObjectForType(_underBackgroundPrefab.name, false);
                    bgClone.transform.position = new Vector2(_startPos.x + (x * _width), _startPos.y - (y * _height));
                    bgClone.transform.SetParent(transform);
                }
            }
        }
    }
}