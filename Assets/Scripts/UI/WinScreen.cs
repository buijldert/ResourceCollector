/*
	WinScreen.cs
	Created 10/2/2017 10:46:32 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace UI
{
	public class WinScreen : MonoBehaviour 
	{
        [SerializeField]
        private GameObject _winScreen;

        private void OnEnable()
        {
            ShopItems.OnWinConditionMet += ShowWinScreen;
        }

        private void OnDisable()
        {
            ShopItems.OnWinConditionMet -= ShowWinScreen;
        }

        private void ShowWinScreen()
        {
            _winScreen.SetActive(true);
        }
	}
}