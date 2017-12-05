/*
	SparkleCooldown.cs
	Created 10/4/2017 12:11:22 PM
	Project Resource Collector by Base Games
*/
using System.Collections;
using UnityEngine;

namespace Utility
{
	public class SparkleCooldown : MonoBehaviour 
	{
        private Animator _sparkleAnimator;
        private SpriteRenderer _sparkleSpriteRenderer;

        [SerializeField] private float _animDuration;

        private Coroutine _cooldownCoroutine;

        private void OnEnable()
        {
            _sparkleAnimator = GetComponent<Animator>();
            _sparkleSpriteRenderer = GetComponent<SpriteRenderer>();
            _cooldownCoroutine = StartCoroutine(Cooldown());
        }

        private void OnDisable()
        {
            StopCoroutine(_cooldownCoroutine);
        }

        private IEnumerator Cooldown()
        {
            _sparkleSpriteRenderer.enabled = true;
            _sparkleAnimator.SetTrigger("SparkleTrigger");
            yield return new WaitForSeconds(_animDuration);
            _sparkleSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            _cooldownCoroutine = StartCoroutine(Cooldown());
        }
    }
}