/*
	SwitchAnimation.cs
	Created 10/4/2017 9:19:14 AM
	Project Resource Collector by Base Games
*/
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
	public class SwitchAnimation : MonoBehaviour 
	{
        [SerializeField]
        private Animator _playerAnimator;

        [SerializeField]
        private List<RuntimeAnimatorController> _animators = new List<RuntimeAnimatorController>();

        private void ChangeAnimation(int animatorIndex)
        {
            _playerAnimator.runtimeAnimatorController = _animators[animatorIndex];
        }
    }
}