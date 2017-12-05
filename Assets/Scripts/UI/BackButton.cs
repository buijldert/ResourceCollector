/*
	BackButton.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using Utility;

namespace UI
{
    public class BackButton : MonoBehaviour
    {
        //The confirmation screen for exiting the scene/app.
        [SerializeField]
        private GameObject _confirmationScreen;

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameState.CGameState == CurrentGameState.Playing)
            {
                if (!_confirmationScreen.activeSelf)
                    _confirmationScreen.SetActive(true);
                else
                    _confirmationScreen.SetActive(false);
            }
        }
    }
}