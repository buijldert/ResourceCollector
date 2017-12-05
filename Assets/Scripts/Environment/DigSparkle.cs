/*
	DigSparkle.cs
	Created 10/10/2017 4:12:08 PM
	Project Resource Collector by Base Games
*/
using PlayerScripts;
using UnityEngine;

namespace Environment
{
	public class DigSparkle : MonoBehaviour 
	{
        [SerializeField] private Vector2 _leftPos, _rightPos;
        private Vector2 _startPos;
        [SerializeField] private Animator _playerAnimator;

        private Animator _digSparkleAnimator;
        private SpriteRenderer _sr;

        private void Start()
        {
            _digSparkleAnimator = GetComponent<Animator>();
            _sr = GetComponent<SpriteRenderer>();
            _startPos = transform.localPosition;
        }

        private void Update()
        {
            if(_playerAnimator.GetBool("IsFacingRight"))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if(_playerAnimator.GetBool("IsDrilling"))
            {
                if (_playerAnimator.GetBool("IsDrillingDown"))
                {
                    if (_playerAnimator.GetBool("IsFacingRight"))
                        transform.localPosition = _rightPos;
                    else
                        transform.localPosition = _leftPos;
                }
                _digSparkleAnimator.enabled = true;
                _sr.enabled = true;
            }
            else
            {
                transform.localPosition = _startPos;
                _digSparkleAnimator.enabled = false;
                _sr.enabled = false;
            }
        }
    }
}