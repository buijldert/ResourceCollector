/*
	EnemyAttack.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using Data;
using System.Collections;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        //An event to be sent out when the enemy attacks.
        public delegate void EnemyAttackAction(float healthMutation);
        public static event EnemyAttackAction OnEnemyAttack;

        //A check whether the enemy is attacking for usage with cooldowngating.
        private bool _isAttacking;

        private bool _hasTarget;
        public bool HasTarget
        {
            get { return _hasTarget; }
            set { _hasTarget = value; }
        }

        //The damage that the enemy does to the player.
        [SerializeField]
        private float _enemyDamage = 4;

        [SerializeField]
        private Animator _enemyAnimator;

        [SerializeField]
        private BoxCollider2D _box2D;

        [SerializeField] private EnemyHealth _enemyHealth;

        private void OnEnable()
        {
            _box2D = GetComponent<BoxCollider2D>();
            StartCoroutine(AttackDelay());
        }

        /// <summary>
        /// Makes the enemy attack when it collides with the player during its attack phase.
        /// </summary>
        /// <param name="collision">The collider that this object collided with.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == InlineStrings.PLAYERTAG && _isAttacking == true && GameState.CGameState == CurrentGameState.Playing && !_enemyHealth.IsDying)
            {
                if (OnEnemyAttack != null)
                    OnEnemyAttack(-_enemyDamage);

                _isAttacking = false;
            }
        }

        private IEnumerator AttackDelay()
        {
            if (_hasTarget && GameState.CGameState != CurrentGameState.Paused)
            {
                yield return new WaitForSeconds(0.5f);
                SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["EnemyRawr"/*xD*/], pitch: Random.Range(2.7f, 3f));
                _enemyAnimator.SetTrigger("AttackTrigger");
                yield return new WaitForSeconds(0.25f);
                _isAttacking = true;
                _box2D.enabled = true;
                yield return null;
                _isAttacking = false;
                _box2D.enabled = false;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
            if(!_enemyHealth.IsDying)
                StartCoroutine(AttackDelay());
        }
    }
}
