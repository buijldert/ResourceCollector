/*
	LoadingScreen.cs
	Created 10/17/2017 9:55:28 AM
	Project Resource Collector by Base Games
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utility
{
	public class LoadingScreen : MonoBehaviour 
	{
        [SerializeField] private List<DoTweenMoveTransform> _transformsToMove;

        private void OnEnable()
        {
            for (int i = 0; i < _transformsToMove.Count; i++)
            {
                _transformsToMove[i].gameObject.SetActive(true);
            }

            Open();
        }

        public void Open(string levelToLoad = "")
        {
            for (int i = 0; i < _transformsToMove.Count; i++)
            {
                _transformsToMove[i].Open();
            }
            LoadLevel(levelToLoad);
        }

        public void Close(string levelToLoad = "")
        {
            for (int i = 0; i < _transformsToMove.Count; i++)
            {
                _transformsToMove[i].Close();
            }
            LoadLevel(levelToLoad);
        }

        public void LoadLevel(string levelToLoad = "")
        {
            if (levelToLoad != "")
            {
                GetComponent<Image>().raycastTarget = true;
                StartCoroutine(LoadDelay(levelToLoad));
            }  
        }

        private IEnumerator LoadDelay(string levelToLoad = "")
        {
            yield return new WaitForSeconds(_transformsToMove[0]._timeToTween);
            SceneManager.LoadScene(levelToLoad);
        }
	}
}