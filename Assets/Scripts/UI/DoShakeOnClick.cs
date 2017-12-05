/*
	DoShakeOnClick.cs
	Created 10/23/2017 3:04:10 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using DG.Tweening;

namespace UI
{
	public class DoShakeOnClick : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _startPos;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPos = _rectTransform.anchoredPosition;
        }

        public void ShakeObject()
        {
            Tween shakeTween = _rectTransform.DOShakePosition(1f, strength:40);
            TweenCallback callback = CallbackFunction;
            shakeTween.OnComplete(callback);
        }

        private void CallbackFunction()
        {
            _rectTransform.DOAnchorPos(_startPos, 0.25f);
        }
    }
}