/*
	PlayerTakeDamageVisual.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.UI;
using Data;
using System.Collections;
using System;

namespace UI
{
    public class Gold : MonoBehaviour
    {
        //The text component that the amount of gold will be displayed.
        [SerializeField]
        private Text _goldText;

        private float _gold;
        private float _updatedGold;

        private float _updateTime = 1f;

        private Coroutine _lerpGoldCoroutine;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void Start()
        {
            StartCoroutine(StartDelay());
        }

        private IEnumerator StartDelay()
        {
            yield return new WaitForEndOfFrame();
            _goldText.text = PlayerStats.Gold.ToString();
        }

        /// <summary>
        /// Mutates the amount of gold by the given amount.
        /// </summary>
        /// <param name="goldMutation">The given mutation.</param>
        public void MutateGold(int goldMutation)
        {
            _gold = PlayerStats.Gold;
            _updatedGold = _gold + goldMutation;
            PlayerStats.Gold = (int)_updatedGold;

            if (_lerpGoldCoroutine != null)
                StopCoroutine(_lerpGoldCoroutine);

            _lerpGoldCoroutine = StartCoroutine(LerpGold());
        }

        private IEnumerator LerpGold()
        {
            float elapsedTime = 0;

            while (elapsedTime < _updateTime)
            {
                _gold = Mathf.Lerp(_gold, _updatedGold, (elapsedTime / _updateTime));
                _goldText.text = (Math.Round((decimal)_gold, 0)).ToString();
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}