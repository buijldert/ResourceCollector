/*
	DoTweenColor.cs
	Created 10/19/2017 4:36:51 PM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;

namespace Utility
{
	public class DoTweenColor : MonoBehaviour 
	{
        private SpriteRenderer _sr;

        private Sequence _colorSequence;

        private Color32 _startColor;
        private Color32 _endColor = new Color32(118, 255, 130, 255);

        [SerializeField] private bool _playOnEnable = false;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            if (_playOnEnable)
                TweenColor();
        }

        public void TweenColor()
        {
            _startColor = _sr.color;
            _colorSequence = DOTween.Sequence();
            _colorSequence.Append(_sr.DOColor(_endColor, .5f));
            _colorSequence.Append(_sr.DOColor(_startColor, .5f));
            _colorSequence.SetLoops(-1);
        }

        public void StopTweeningColor()
        {
            _colorSequence.Kill();
        }
	}
}