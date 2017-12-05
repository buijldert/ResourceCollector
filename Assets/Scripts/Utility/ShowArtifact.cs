/*
	ShowArtifact.cs
	Created 10/19/2017 3:47:56 PM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using Serialization;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
	public class ShowArtifact : MonoBehaviour 
	{
        [SerializeField] private GameObject _howToPlayScreen;
        [SerializeField] private GameObject _skipButton;

        private Transform _artifactTransform;

        private Coroutine _lateStartCoroutine;

        private float _timeToMove = 5f;
        private float _timeToZoom = 1f;

        private Sequence _showSequence;

        private Vector3 _startPos;

        private bool _isArtifactShown = false;

        private void Start()
        {
            _startPos = transform.position;
            if (!_howToPlayScreen.activeSelf)
            {
                StartShowArtifact();
            }  
        }

        public void StartShowArtifact()
        {
            if(!_isArtifactShown && SceneManager.GetActiveScene().name == "Main")
                _lateStartCoroutine = StartCoroutine(ShowArtifactCoroutine());
        }

        private IEnumerator ShowArtifactCoroutine()
        {
            _isArtifactShown = true;
            GameState.CGameState = CurrentGameState.Paused;
            yield return new WaitForEndOfFrame();
            _artifactTransform = GameObject.Find("Artifact").transform;
            if (LoadLevel.SaveFileExists)
            {
                ResetAnimation();
            }
            else
            {
                _skipButton.SetActive(true);
                
                _artifactTransform.gameObject.GetComponent<DoTweenColor>().TweenColor();

                _showSequence = DOTween.Sequence();

                _showSequence.AppendInterval(1f);
                _showSequence.Append(Camera.main.DOOrthoSize(5, _timeToZoom));//set ease
                _showSequence.Append(transform.DOMove(new Vector3(Mathf.Clamp(_artifactTransform.position.x, -15f, 16f), _artifactTransform.position.y + 4.5f, -20f), _timeToMove).SetEase(Ease.InOutFlash));

                _showSequence.Append(Camera.main.DOOrthoSize(3, _timeToZoom));
                _showSequence.Join(transform.DOMove(new Vector3(Mathf.Clamp(_artifactTransform.position.x, -19f, 20f), _artifactTransform.position.y + 2.5f, -20f), _timeToZoom));
                _showSequence.AppendInterval(3f);
                _showSequence.Append(Camera.main.DOOrthoSize(5, _timeToZoom));
                _showSequence.Join(transform.DOMove(new Vector3(Mathf.Clamp(_artifactTransform.position.x, -15f, 16f), _artifactTransform.position.y + 4.5f, -20f), _timeToZoom));
                _showSequence.Append(transform.DOMove(_startPos, _timeToMove/_timeToMove).SetEase(Ease.InOutFlash));
                _showSequence.Append(Camera.main.DOOrthoSize(3, _timeToZoom));

                _showSequence.SetLoops(1);

                while(_showSequence.IsPlaying())
                {
                    yield return null;
                    GameState.CGameState = CurrentGameState.Paused;
                }

                yield return _showSequence.WaitForCompletion();
                ResetAnimation();
            }
        }

        public void ResetAnimation()
        {
            StopCoroutine(_lateStartCoroutine);
            _showSequence.Kill();
            _skipButton.SetActive(false);
            StartCoroutine(ReturnCamera());
        }

        private IEnumerator ReturnCamera()
        {
            Sequence returnSequence = DOTween.Sequence();
            returnSequence.Append(transform.DOMove(_startPos, 1f));
            returnSequence.Append(GetComponent<Light>().DOIntensity(0, 1f));
            returnSequence.Join(Camera.main.DOOrthoSize(3, 1f));
            returnSequence.SetLoops(1);

            yield return returnSequence.WaitForCompletion();
            GameState.CGameState = CurrentGameState.Playing;

            GetComponent<CameraFollow>().enabled = true;
            _artifactTransform.gameObject.GetComponent<DoTweenColor>().StopTweeningColor();
            
        }
    }
}