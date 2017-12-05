/*
	HoverPickup.cs
	Created 10/25/2017 3:23:54 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Utility
{
	public class HoverPickup : MonoBehaviour 
	{
        private Sequence _hoverTween;
        private Rigidbody2D _rb2d;
        private bool _isHovering;
        private PickupGroundChecker _pickupGroundChecker;
        private BoxCollider2D _collider;
        private Coroutine _startHovering;
        public Coroutine StartHovering{ get { return _startHovering; } }

        private void OnEnable()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _collider = GetComponentInChildren<BoxCollider2D>();
            _pickupGroundChecker = GetComponentInChildren<PickupGroundChecker>();
            _rb2d.gravityScale = 1f;
            _startHovering = StartCoroutine(StartHover());
        }

        IEnumerator StartHover()
        {
            if (_pickupGroundChecker.PickupIsGrounded && !_isHovering)
            {
                Hover();
                _rb2d.gravityScale = 0;
                _collider.enabled = false;
                
            }
            else if (!_pickupGroundChecker.PickupIsGrounded && _isHovering)
            {
                _hoverTween.Kill();
                _isHovering = false;
                _collider.enabled = true;
                yield return new WaitForSeconds(0.05f);
                _rb2d.gravityScale = 1;
            }
            yield return new WaitForEndOfFrame();
            _startHovering = StartCoroutine(StartHover());
        }

        private void Hover()
        {
            _isHovering = true;
            Vector2 targetPos = new Vector2(transform.position.x, transform.position.y + 0.08f);
            _hoverTween = DOTween.Sequence();
            _hoverTween.Append(transform.DOMoveY(targetPos.y, .5f));
            _hoverTween.SetLoops(-1, loopType:LoopType.Yoyo);
        }

        public void KillHover()
        {
            StopCoroutine(_startHovering);
            _rb2d.gravityScale = 0;
        }
	}
}