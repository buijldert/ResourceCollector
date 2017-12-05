/*
	OpenSaveHouse.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using Data;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace UI
{
    public class OpenSaveHouse : MonoBehaviour
    {
        //A check if the savehouse can be opened.
        private bool _canOpenSaveHouse;

        //The saving menu gameobject.
        [FormerlySerializedAs("_saveHouse")]
        [SerializeField] private GameObject _saveScreen;
        //The button that appears when the player is close enough to the savehouse.
        [SerializeField] private GameObject _openSaveButton;

        /// <summary>
        /// Makes sure the savescreen can be opened when the player is close enough.
        /// </summary>
        /// <param name="other">The object that the savehouse is colliding with.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            _canOpenSaveHouse = true;
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (_canOpenSaveHouse)
            {
                _openSaveButton.SetActive(true);
            }
            else
            {
                _openSaveButton.SetActive(false);
            }
#if !MOBILE_INPUT
            if (_canOpenSaveHouse && Input.GetKeyDown(KeyCode.Space) && !_saveScreen.activeSelf && GameState.CGameState == CurrentGameState.Playing)
            {
                _saveScreen.SetActive(true);
            }
            else if (_saveScreen.activeSelf && Input.GetKeyDown(KeyCode.Space))
            {
                _saveScreen.SetActive(false);
            }
#endif
        }

        /// <summary>
        /// Makes sure the player cant open the savescreen when he isnt close enough to the house.
        /// </summary>
        /// <param name="other">The object that the savehouse is colliding with.</param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == InlineStrings.PLAYERTAG && _canOpenSaveHouse)
            {
                _canOpenSaveHouse = false;
                _saveScreen.SetActive(false);
            }
        }
    }
}