/*
	TweenText.cs
	Created 10/2/2017 9:04:58 AM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class TweenText : MonoBehaviour 
	{
        [Tooltip("The text component that will be tweened. The text contents of this component will be tweened.")]
        [SerializeField] private Text _textToTween;

        [Tooltip("The delay after which the text will be tweened.")]
        [SerializeField] private float _tweenDelay;
        [Tooltip("The duration of the text animation.")]
        [SerializeField] private float _tweenDuration;

        [Tooltip("Whether rich text should be enabled for the tweened.")]
        [SerializeField] private bool _richTextEnabled;
        [Tooltip("Whether the text should start tweening when the component is enabled.")]
        [SerializeField] private bool _tweenOnEnable;


        [Tooltip("The mode in which the text will be scrambled.")]
        [SerializeField] private ScrambleMode _scrambleMode;

        private string _textContent;

        private void OnEnable()
        {
            if (_tweenOnEnable)
                StartTweening();
        }

        public void StartTweening()
        {
            _textContent = _textToTween.text;
            _textToTween.text = string.Empty;
            StartCoroutine(TextTweenDelay());
        }

        private IEnumerator TextTweenDelay()
        {
            yield return new WaitForSeconds(_tweenDelay);
            _textToTween.DOText(_textContent, _tweenDuration, _richTextEnabled, _scrambleMode);
        }
    }
}