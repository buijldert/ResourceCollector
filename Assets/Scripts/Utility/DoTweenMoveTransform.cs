/*
	DoTweenMoveTransform.cs
	Created 10/17/2017 9:28:38 AM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(RectTransform))]
	public class DoTweenMoveTransform : MonoBehaviour 
	{
        public float _timeToTween = 0.5f;
        [SerializeField] private Vector2 _openedPos;
        [SerializeField] private Vector2 _closedPos;

        private RectTransform _rt;

        private void OnEnable()
        {
            _rt = GetComponent<RectTransform>();
        }

        public void Open()
        {
            _rt.localPosition = _closedPos;
            _rt.DOAnchorPos(_openedPos, _timeToTween);
        }

        public void Close()
        {
            _rt.localPosition = _openedPos;
            _rt.DOAnchorPos(_closedPos, _timeToTween);
        }
    }
}