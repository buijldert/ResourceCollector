/*
	ControlGameState.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;

namespace Utility
{
    public class ControlGameState : MonoBehaviour
    {
        [SerializeField]
        private GameObject _uiBlur;
        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            _uiBlur.SetActive(true);
            GameState.CGameState = CurrentGameState.Paused;
            transform.SetAsLastSibling();
        }

        /// <summary>
        /// Use this for de-initialization.
        /// </summary>
        private void OnDisable()
        {
            _uiBlur.SetActive(false);
            GameState.CGameState = CurrentGameState.Playing;
        }
    }
}