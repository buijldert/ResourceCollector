/*
	InstructionScreen.cs
	Created 10/5/2017 11:31:30 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using Extensions;
using UnityEngine.UI;
using Data;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InstructionScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject _instructionScreen;

        [SerializeField]
        private Toggle _instructionToggle;

        private bool _enableInstructions;

        private void OnEnable()
        {
            _enableInstructions = PlayerPrefs2.GetBool(InlineStrings.INSTRUCTIONSTATUS);
            _instructionToggle.isOn = !_enableInstructions;
            if(SceneManager.GetActiveScene().name == "Main")
                _instructionScreen.SetActive(_enableInstructions);
        }

        public void SetToggle()
        {
            PlayerPrefs2.SetBool(InlineStrings.INSTRUCTIONSTATUS, !_instructionToggle.isOn);
        }
    }
}