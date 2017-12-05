/*
	PlayerAnimations.cs
	Created 9/29/2017 10:39:19 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace PlayerScripts
{
	public class PlayerAnimations : MonoBehaviour 
	{
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void MoveAnimation(bool isMoving, string direction)
        {
            _animator.SetBool(direction, isMoving);
        }
    }
}