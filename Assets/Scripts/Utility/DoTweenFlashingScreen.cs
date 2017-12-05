/*
	DoTweenFlashingScreen.cs
	Created 10/9/2017 9:24:26 AM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
	public class DoTweenFlashingScreen : MonoBehaviour 
	{
        [SerializeField]
        private Image _flashingScreenImage;

        [SerializeField]
        private Color32 startColor = new Color32(66, 134, 244, 75);
        [SerializeField]
        private Color32 endColor = new Color32(10, 57, 132, 75);

        private void OnEnable()
        {
            Sequence flashingSequence = DOTween.Sequence();
            _flashingScreenImage.enabled = true;
            flashingSequence.Append(_flashingScreenImage.DOColor(endColor, .5f));
            flashingSequence.Append(_flashingScreenImage.DOColor(startColor, .5f));
            flashingSequence.SetLoops(-1);
        }
    }
}