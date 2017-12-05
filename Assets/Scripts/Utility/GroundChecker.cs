/*
	GroundChecker.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;

namespace Utility
{
    public class GroundChecker : MonoBehaviour
    {
        //The transform of the ground check.
        [SerializeField] private Transform _groundCheck;

        //The radius of the ground check.
        [SerializeField] private float _groundCheckRadius;

        //The ground layer.
        [SerializeField] private LayerMask _groundLayer;

        //A check if the player is grounded.
        public static bool IsGrounded;

        /// <summary>
        /// FixedUpdate is called every fixed framerate frame.
        /// </summary>
        private void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }
    }
}