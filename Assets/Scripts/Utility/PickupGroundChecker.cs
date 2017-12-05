/*
	PickupGroundChecker.cs
	Created 10/25/2017 4:08:17 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace Utility
{
	public class PickupGroundChecker : MonoBehaviour 
	{
            //The transform of the ground check.
            [SerializeField] private Transform _groundCheck;

            //The radius of the ground check.
            [SerializeField] private float _groundCheckRadius;

            //The ground layer.
            [SerializeField] private LayerMask _groundLayer;

            //A check if the player is grounded.
            public bool PickupIsGrounded;

            /// <summary>
            /// FixedUpdate is called every fixed framerate frame.
            /// </summary>
            private void FixedUpdate()
            {
                PickupIsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
            }
    }
}