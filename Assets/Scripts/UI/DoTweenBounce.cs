/*
	DoTweenBounce.cs
	Created 10/1/2017 8:14:43 PM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;

namespace UI
{
	public class DoTweenBounce : MonoBehaviour 
	{
        [SerializeField] private float _timeToEnlarge = 0.3f;
        [SerializeField] private float _timeToBounceBack = 0.15f;

        [Tooltip("-1 = infinite loops 0 = doesn't play")]
        [SerializeField] private int _numberOfLoops = 1;

        [SerializeField] private bool _playOnEnable;

        [SerializeField] private Vector3 _beginScale = new Vector3(0.1f, 0.1f, 0.1f), _bouncedScale = new Vector3(1.1f, 1.1f, 1.1f);

        Sequence bounceSequence;

        private void OnEnable()
        {
            if (_playOnEnable)
                Bounce();
        }

        private void OnDisable()
        {
            bounceSequence.Kill();
        }

        public void Bounce()
        {
            bounceSequence = DOTween.Sequence();
            transform.localScale = _beginScale;
            bounceSequence.Append(transform.DOScale(_bouncedScale, _timeToEnlarge));
            bounceSequence.Append(transform.DOScale(Vector3.one, _timeToBounceBack));
            bounceSequence.SetLoops(_numberOfLoops);
        }
    }
}