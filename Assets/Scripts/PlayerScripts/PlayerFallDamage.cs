/*
	PlayerFallDamage.cs
	Created 10/17/2017 1:39:49 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using Utility;

namespace PlayerScripts
{
	public class PlayerFallDamage : MonoBehaviour 
	{
        public delegate void OnFallDamage(float healthMutation);
        public static event OnFallDamage TakeFallDamage;

        private Rigidbody2D _rgb2d;
        private float _velocity = 0;

        private void Awake()
        {
            _rgb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!GroundChecker.IsGrounded)
            {
                CalculateVelocity();
            }

            if(GroundChecker.IsGrounded)
            {
                TakeDamage();
            }
        }

        private float CalculateDamage()
        {
            if (_velocity < -6)
            {
                float tempDamage = (_velocity / 4) * Mathf.Abs(_velocity/5);
                float damage = Mathf.RoundToInt(tempDamage);
                return damage;
            }
            else
                return 0;
        }

        private void CalculateVelocity()
        {
            if(_rgb2d.velocity.y < 0 && _rgb2d.velocity.y < _velocity)
            {
                _velocity = _rgb2d.velocity.y;
            }
        }

        private void TakeDamage()
        {
            if (CalculateDamage() < 0)
                if (TakeFallDamage != null)
                    TakeFallDamage(CalculateDamage());

            ResetVelocity();
        }

        public void ResetVelocity()
        {
            _velocity = 0;
        }
    }
}