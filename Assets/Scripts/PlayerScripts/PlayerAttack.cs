/*
	PlayerAttack.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Interfaces;
using Utility;
using Data;
using DG.Tweening;

namespace PlayerScripts
{
    public class PlayerAttack : MonoBehaviour
    {
        //The boxcollider attached to the PlayerSprite GameObject.
        [SerializeField]
        private BoxCollider2D _box2D;

        //A check if the player is attacking so a cooldown can be enforced.
        private bool _isAttacking;

        //The duration of an attack.
        [SerializeField] private float _attackDuration = 0.25f;
        //The cooldown of the attack.
        [SerializeField] private float _attackCooldown = 0.75f;
        //The damage of the attack;
        [SerializeField] private float _attackDamage = -2.5f;

        //The image representing the cooldown of the attack.
        [SerializeField] private Image _cooldownImage;

        [SerializeField] private Animator _playerAnimator;

        private Coroutine _attackCooldownCoroutine;

        /// <summary>
        /// Launches an attack in the direction the player is facing.
        /// </summary>
        public void Attack()
        {
            if (_isAttacking == false && GameState.CGameState == CurrentGameState.Playing && GroundChecker.IsGrounded)
            {
                _box2D.enabled = true;
                _isAttacking = true;
                PlayerMovement.CanMove = false;
                _attackCooldownCoroutine = StartCoroutine(AttackCooldown());
            }
        }

        /// <summary>
        /// The cooldown of the player attack.
        /// </summary>
        private IEnumerator AttackCooldown()
        {
            _cooldownImage.fillAmount = 1f;
            _cooldownImage.DOFillAmount(0f, _attackCooldown + _attackDuration);
            _playerAnimator.SetTrigger("AttackTrigger");
            yield return new WaitForSeconds(_attackDuration);
            PlayerMovement.CanMove = true;
            _box2D.enabled = false;
            yield return new WaitForSeconds(_attackCooldown);
            _isAttacking = false;
        }

        /// <summary>
        /// Checks if the player is colliding with the enemy so the attack can do damage.
        /// </summary>
        /// <param name="collision">The object the playerattack collider is colliding with.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == InlineStrings.ENEMYTAG)
            {
                ExecuteEvents.Execute<IDamageable>(collision.gameObject, null, (x, y) => x.TakeDamage(_attackDamage));
            }
        }


        /// <summary>
        /// Update is called once every frame.
        /// </summary>
        private void Update()
        {
            if (_playerAnimator.GetBool("IsFacingRight"))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

#if !MOBILE_INPUT
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
#endif
        }
    }
}