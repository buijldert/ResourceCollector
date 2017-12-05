/*
	ShopTab.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class ShopTab : MonoBehaviour
    {
        //An event to be sent out when the tab is clicked.
        public delegate void TabClickedAction();
        public static event TabClickedAction OnTabClicked;

        //The gameobject of the tab.
        [SerializeField]
        private GameObject _tab;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            OnTabClicked += DisableTab;
        }

        /// <summary>
        /// Use this for de-initialization.
        /// </summary>
        private void OnDisable()
        {
            OnTabClicked -= DisableTab;
        }

        /// <summary>
        /// Enables this tabs contents and disables the content of all others.
        /// </summary>
        public void EnableTab()
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["TabClickSound"], pitch: Random.Range(0.9f, 1.1f), volume:0.15f);
            if (!_tab.activeSelf)
            {
                if (OnTabClicked != null)
                    OnTabClicked();

                _tab.SetActive(true);
                GetComponent<Outline>().enabled = true;
            }
        }

        /// <summary>
        /// Disables this tabs contents.
        /// </summary>
        private void DisableTab()
        {
            _tab.SetActive(false);
            GetComponent<Outline>().enabled = false;
        }
    }
}