/*
	DoTextAlpha.cs
	Created 10/23/2017 1:23:21 PM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
	public class DoTextAlpha : MonoBehaviour 
	{
        [SerializeField] private Text _textToFade;

        private void Start()
        {
            Sequence alphaSequence = DOTween.Sequence();
            alphaSequence.Append(_textToFade.DOFade(.1f, 1f));
            alphaSequence.Append(_textToFade.DOFade(1f, 1f));
            TweenCallback onCompCallback = ResetAlpha;
            alphaSequence.OnComplete(onCompCallback);
            alphaSequence.SetLoops(2);
        }

        private void ResetAlpha()
        {
            _textToFade.DOFade(0f, .5f);
        }
    }
}