/*
	EnemyMovement.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using Data;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        //The transform of the player so the enemy can follow him along the x axis.
        private Transform _player;

        //The speed at which the enemy will follow the player.
        private float _moveSpeed = 1f;
        //The maximum distance over the x axis to the player.
        private float _distanceToStop = 0.5f;
        private float _middleOffset = 0.25f;

        //The rigidbody atttached to the enemy.
        private Rigidbody2D _rb2D;

        private Animator _enemyAnimator;

        private AnimatorStateInfo _animatorState;

        private EnemyHealth _enemyHealth;

        private void OnEnable()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _enemyAnimator = GetComponent<Animator>();
            _player = GameObject.FindWithTag(InlineStrings.PLAYERTAG).transform;
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        /// <summary>
        /// Follows the target, in this case the player, over the X axis and rotates towards the player.
        /// </summary>
        private void FollowTargetXWithRotation()
        {
            _animatorState = _enemyAnimator.GetCurrentAnimatorStateInfo(0);
            if (transform.position.x > _player.position.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);


            if (_player == null)
                _player = GameObject.FindWithTag(InlineStrings.PLAYERTAG).transform;

            if (Mathf.Abs(_player.position.x - _middleOffset - transform.position.x) > _distanceToStop && !_animatorState.IsName("EnemyAttack"))
            {
                _enemyAnimator.SetBool("IsMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(_player.position.x, transform.position.y), Time.deltaTime * _moveSpeed);
                
            }
            else if(!_animatorState.IsName("EnemyAttack"))
                _enemyAnimator.SetBool("IsMoving", false);
        }

        /// <summary>
        /// FixedUpdate is called every fixed framerate frame.
        /// </summary>
        private void FixedUpdate()
        {
            if (GameState.CGameState != CurrentGameState.Paused && !_enemyHealth.IsDying)
                FollowTargetXWithRotation();
            else
                _rb2D.velocity = Vector2.zero;
        }
    }
}
