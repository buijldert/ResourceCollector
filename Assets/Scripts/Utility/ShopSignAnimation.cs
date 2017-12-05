/*
	ShopSignAnimation.cs
	Created 10/16/2017 10:38:02 AM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;

namespace Utility
{
	public class ShopSignAnimation : MonoBehaviour 
	{
        private SpriteRenderer _sr;
        
        private Color32 _startColor;
        private Color32 _endColor = new Color32(118,255,130,100);

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _startColor = _sr.color;

            Sequence fadeSequence = DOTween.Sequence();

            fadeSequence.Append(_sr.DOColor(_endColor, 1f));
            fadeSequence.Append(_sr.DOColor(_startColor, 1f));
            fadeSequence.SetLoops(-1);
        }
    }
}