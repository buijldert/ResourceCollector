/*
	HowToPlay.cs
	Created 10/12/2017 2:23:36 PM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class HowToPlay : MonoBehaviour 
	{
		[SerializeField] private List<Sprite> _pages;

        [SerializeField] private List<Graphic> _UIGraphics;

        private Tween _changeTween;

        private Image _imageToChange;

        private int _currentPage;

        [SerializeField] private Button _previousButton, _nextButton;

        private Coroutine _changeCoroutine;

        private float _fadeTime = 1.5f;

        private void Awake()
        {
#if MOBILE_INPUT
            _pages.RemoveAt(0);
            _imageToChange = GetComponent<Image>();
            _imageToChange.sprite = _pages[0];
            _previousButton.gameObject.SetActive(false);
            _nextButton.gameObject.SetActive(false);
#endif
        }

        private void OnEnable()
        {

            
            PageChange();
        }

        public void PreviousPage()
        {
            _currentPage -= 1;
            StartChangeCoroutine();
        }

        public void NextPage()
        {
            _currentPage += 1;
            StartChangeCoroutine();
        }

        private void PageChange()
        {
            if (_currentPage > 0)
            {
                _previousButton.interactable = true;
            }
            else
            {
                _previousButton.interactable = false;
            }

            if (_currentPage < _pages.Count - 1)
            {
                _nextButton.interactable = true;
            }
            else
            {
                _nextButton.interactable = false;
            }
        }

        private void StartChangeCoroutine()
        {
            _changeCoroutine = StartCoroutine(ChangePage());
        }

        private IEnumerator ChangePage()
        {
            PageChange();
            for (int i = 0; i < _UIGraphics.Count; i++)
            {
                _UIGraphics[i].DOFade(0f, _fadeTime);
            }
            yield return new WaitForSeconds(_fadeTime);
            _imageToChange.sprite = _pages[_currentPage];
            for (int i = 0; i < _UIGraphics.Count; i++)
            {
                _UIGraphics[i].DOFade(1f, _fadeTime);
            }

        }
    }
}