/*
	RefuelReminder.cs
	Created 10/11/2017 1:17:34 PM
	Project Resource Collector by Base Games
*/
using Data;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.EventSystems;

namespace UI
{
	public class RefuelReminder : MonoBehaviour 
	{
        [SerializeField]private Transform _objectToBounce;
        private Tween BounceTween;
        private bool _isBouncing;

        private void OnEnable()
        {
            CheckFuel();
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != this.gameObject)
            {
                if (BounceTween != null && !BounceTween.IsPlaying())
                {
                    CheckFuel();
                }
            }
        }

        void CheckFuel()
        {
            if(PlayerStats.Fuel < (PlayerStats.MaxFuel / 2))
            {
                BounceUp();
            }
            else
            {
                ResetScale();
            }
        }

        void BounceDown()
        {
            BounceTween = _objectToBounce.DOScale(Vector3.one, 0.5f);
            BounceTween.onComplete += BounceUp;
        }

        void BounceUp()
        {
            BounceTween = _objectToBounce.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
            BounceTween.onComplete += BounceDown;
        }

        public void ResetScale()
        {
            BounceTween.Kill();
            _objectToBounce.DOScale(Vector3.one, 0.25f);
        }
	}
}