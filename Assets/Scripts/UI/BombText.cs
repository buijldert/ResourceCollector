/*
	BombText.cs
	Created 10/12/2017 3:14:44 PM
	Project Resource Collector by Base Games
*/
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BombText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Update()
        {
            _text.text = PlayerStats.AmountOfBombs.ToString();
        }
    }
}