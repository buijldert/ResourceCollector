/*
	ShopUIScale.cs
	Created 10/9/2017 9:29:16 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace UI
{
    public class ShopUIScale : MonoBehaviour
    {
        [SerializeField] private List<Transform> _objectsToScale = new List<Transform>();
        private Coroutine _scaleElementsCoroutine;

        public Tween _scaleElementsTween;

        [SerializeField] private float _timeToTween, _timeBetweenTween = 0.1f;

        private void OnEnable()
        {
            OpenShop.OnShopClose += ResetScale;
            _scaleElementsCoroutine = StartCoroutine(ScaleElements());
        }

        private void OnDisable()
        {
            OpenShop.OnShopClose -= ResetScale;
            StopCoroutine(_scaleElementsCoroutine);
            ResetScale();
        }

        private IEnumerator ScaleElements()
        {
            for (int j = 0; j < _objectsToScale.Count; j++)
            {
                _objectsToScale[j].localScale = Vector3.zero;
            }
            for (int i = 0; i < _objectsToScale.Count; i++)
            {
                _scaleElementsTween = _objectsToScale[i].DOScale(Vector3.one, _timeToTween);
                yield return new WaitForSeconds(_timeBetweenTween);
            }
        }

        private void ResetScale()
        {
            _scaleElementsTween.Kill();
        }
    }
}