/*
	EnemyHealth.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using Interfaces;
using Utility;
using System.Collections;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        //The health points of the enemy.
        [SerializeField]
        private float _health = 5f;

        [HideInInspector]
        public bool IsDying = false;

        private Animator _animator;

        [SerializeField] private GameObject _droppableBlock;

        private int _enemyDepth;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            StartCoroutine(DepthDelay());
        }

        private IEnumerator DepthDelay()
        {
            yield return new WaitForEndOfFrame();
            _enemyDepth = Mathf.Abs((int)transform.position.y);
        }

        /// <summary>
        /// Mutates the health with the given mutation (+/-).
        /// </summary>
        /// <param name="healthMutation">The number with which the enemy health will be changed.</param>
        public void TakeDamage(float healthMutation)
        {
            _health += healthMutation;

            if (_health <= 0 && IsDying == false)
            {
                IsDying = true;
                _health = 5f;
                _animator.SetTrigger("DeathTrigger");
                StartCoroutine(DyingDelay());
            }
        }

        private IEnumerator DyingDelay()
        {
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
            _health = 5f;
            IsDying = false;

            GameObject dropBlockClone;

            for (int i = 0; i < (_enemyDepth/20) + 1; i++)
            {
                dropBlockClone = ObjectPool.Instance.GetObjectForType(_droppableBlock.name, false);
                dropBlockClone.transform.position = new Vector2(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f), transform.position.y + 0.3f);
            }

            ObjectPool.Instance.PoolObject(gameObject);
        }
    }
}
