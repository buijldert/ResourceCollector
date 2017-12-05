/*
	DestroyOverTime.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections;
using UnityEngine;

namespace Utility
{
    public class PoolOverTime : MonoBehaviour
    {
        //The time it takes to pool the gameobject.
        [SerializeField]
        private float _destroyTime;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            StartCoroutine(DestroyDelay());
        }

        /// <summary>
        /// Pools the gamebject after a delay.
        /// </summary>
        private IEnumerator DestroyDelay()
        {
            yield return new WaitForSeconds(_destroyTime);
            ObjectPool.Instance.PoolObject(gameObject);
        }
    }
}