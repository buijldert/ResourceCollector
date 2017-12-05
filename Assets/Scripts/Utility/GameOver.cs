/*
	GameOver.cs
	Created 10/13/2017 10:29:59 AM
	Project Resource Collector by Base Games
*/
using System.Collections;
using UI;
using UnityEngine;

namespace Utility
{
	public class GameOver : MonoBehaviour 
	{
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private Animator _playerAnimator;

        private void OnEnable()
        {
            FuelBar.OnFuelDepeleted += GameIsOver;
            HealthBar.OnHealthDepleted += GameIsOver;
        }

        private void GameIsOver()
        {
            StartCoroutine(GameOverDelay());
        }

        private IEnumerator GameOverDelay()
        {
            _playerAnimator.SetTrigger("DeathTrigger");
            yield return new WaitForSeconds(_playerAnimator.GetCurrentAnimatorClipInfo(0).Length);
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["PlayerDeath"], volume: 0.5f);
            yield return new WaitForSeconds(.5f);
            _gameOverScreen.SetActive(true);
        }

        private void OnDisable()
        {
            FuelBar.OnFuelDepeleted -= GameIsOver;
            HealthBar.OnHealthDepleted -= GameIsOver;
        }
    }
}