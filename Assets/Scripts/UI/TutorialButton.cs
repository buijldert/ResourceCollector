/*
	TutorialButton.cs
	Created 10/5/2017 2:21:03 PM
	Project Resource Collector by Base Games
*/
using Data;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class TutorialButton : MonoBehaviour 
	{
        [SerializeField]
        private Toggle _tutToggle;

        private bool _isTutorialEnabled = true;

        private void OnEnable()
        {
            _isTutorialEnabled = PlayerPrefs2.GetBool(InlineStrings.INSTRUCTIONSTATUS);

            _tutToggle.isOn = _isTutorialEnabled;
        }

        public void ChangeToggle()
        {
            PlayerPrefs2.SetBool(InlineStrings.INSTRUCTIONSTATUS, _tutToggle.isOn);
        }
    }
}