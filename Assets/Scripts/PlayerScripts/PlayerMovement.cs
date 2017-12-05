/*
	PlayerMovement.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections;
using UnityEngine;
using Utility;
#if MOBILE_INPUT
using UnityStandardAssets.CrossPlatformInput;
#endif

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerFallDamage _playerFallDamage;

        //An event to be sent out anytime the player is moving.
        public delegate void MovingAction(float fuelMutation);
        public static event MovingAction OnMovingAction;

        private bool _isMoving;
        public bool IsMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }
        private string _direction = "IsMovingLeft";
        public PlayerAnimations _animator;

        //The transform of the sprite display.
        [SerializeField] private Transform _sprite;
        private AudioSource _source;
        private bool _canPlaySound = true;

        //The rigidbody2D attached to the player.
        private Rigidbody2D _rb2D;

        public Rigidbody2D RB2D
        {
            get { return _rb2D; }
            set { _rb2D = value; }
        }

        //The speed at which the player moves left and right
        private float _speed = 4f;
        //The fuel reduction per frame.
        private float _fuelReduction = 0.08f;
        //The gravityscale of the rigidbody2D attached to the player.
        private float _gravityScale;
        //The x and y velocity of the player.
        [HideInInspector]
        public float x, y;

        private float _xModifier, _yModifier;
        private float _xIncrease, _yIncrease, _standardIncrease = 0.05f;

        private float _axisGateX = .25f;

        public static bool CanMove = true;

        private bool _isFlyingSoundPlaying;

        private bool _isXAxis = false, _isYAxis = false;

        [SerializeField] private AudioSource _as;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void Start()
        {
            _animator = GetComponentInChildren<PlayerAnimations>();
            _rb2D = GetComponent<Rigidbody2D>();
            _gravityScale = _rb2D.gravityScale;
            _source = GameObject.FindWithTag("SoundObject").GetComponent<AudioSource>();
            _animator.MoveAnimation(true, "IsFacingLeft");
            _playerFallDamage = GetComponent<PlayerFallDamage>();
        }

        /// <summary>
        /// Changes the player y to the given value.
        /// </summary>
        /// <param name="valueY">The given y value.</param>
        public void ChangePlusY(bool isYAxis)
        {
            _isYAxis = isYAxis;
            _yIncrease = _standardIncrease;
        }

        public void ChangePlusX(bool isXAxis)
        {
            _isXAxis = isXAxis;
            _xIncrease = _standardIncrease;
        }

        public void ChangeMinusY(bool isYAxis)
        {
            _isYAxis = isYAxis;
            _yIncrease = -_standardIncrease;
        }

        public void ChangeMinusX(bool isXAxis)
        {
            _isXAxis = isXAxis;
            _xIncrease = -_standardIncrease;
        }

        IEnumerator WalkSound()
        {
            if (_isMoving && _canPlaySound)
            {
                _canPlaySound = false;
                _source.pitch = Random.Range(0.95f, 1.05f);
                _source.Play();
                yield return new WaitForSeconds(_source.clip.length);
                _canPlaySound = true;
            }
        }

        /// <summary>
        /// FixedUpdate is called every fixed framerate frame.
        /// </summary>
        private void FixedUpdate()
        {
            if (GameState.CGameState != CurrentGameState.Paused && CanMove)
            {
                StartCoroutine(WalkSound());

                _rb2D.gravityScale = _gravityScale;
#if MOBILE_INPUT
                if(_isXAxis)
                {
                    _xModifier = Mathf.Clamp(_xModifier += _xIncrease, -1f, 1f); 
                }
                else
                {
                    _xModifier = 0;
                }

                if(_isYAxis)
                {
                    _yModifier = Mathf.Clamp(_yModifier += _yIncrease, -1f, 1f);
                }
                else
                {
                    _yModifier = 0;
                }

                x = _xModifier * _speed;
                y = _yModifier  * _speed;
#else
                x = Input.GetAxis("Horizontal") * _speed;
                y = Input.GetAxis("Vertical") * _speed;
#endif

                if (!GroundChecker.IsGrounded && !_isFlyingSoundPlaying)
                {
                    _as.Play();
                    _isFlyingSoundPlaying = true;
                }
                else if (GroundChecker.IsGrounded && _isFlyingSoundPlaying)
                {
                    _as.Pause();
                    _isFlyingSoundPlaying = false;
                }

                if (x > _axisGateX || x < _axisGateX)
                    _rb2D.velocity = new Vector2(x, _rb2D.velocity.y);
                
                if (y > 0)
                {
                    if (OnMovingAction != null)
                        OnMovingAction(-_fuelReduction);

                    _rb2D.velocity = new Vector2(_rb2D.velocity.x, y);
                    _playerFallDamage.ResetVelocity();
                }

                if (x > 0)
                {
                    _animator.MoveAnimation(true, "IsFacingRight");
                    _animator.MoveAnimation(false, "IsFacingLeft");
                }
                else if (x < 0)
                {
                    _animator.MoveAnimation(true, "IsFacingLeft");
                    _animator.MoveAnimation(false, "IsFacingRight");
                }

                if (x > _axisGateX && GroundChecker.IsGrounded)
                {
                    _isMoving = true;
                    _direction = "IsMovingRight";
                }
                else if (x < -_axisGateX && GroundChecker.IsGrounded)
                {
                    _isMoving = true;
                    _direction = "IsMovingLeft";
                }

                else
                    _isMoving = false;

                if (y > 0 || !GroundChecker.IsGrounded)
                {
                    _animator.MoveAnimation(true, "IsFlying");
                    _animator.MoveAnimation(false, "IsMovingRight");
                    _animator.MoveAnimation(false, "IsMovingLeft");
                }
                else if(y < 0)
                    _animator.MoveAnimation(true, "IsDrillingDown");

                else {_animator.MoveAnimation(false, "IsFlying"); _animator.MoveAnimation(false, "IsDrillingDown"); }

                _animator.MoveAnimation(_isMoving, _direction);
            }
            else
            {
                _animator.MoveAnimation(false, _direction);
                _rb2D.velocity = Vector2.zero;
                _rb2D.gravityScale = 0;
            }
        }
    }
}