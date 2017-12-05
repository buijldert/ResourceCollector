/*
	ShakeTransform.cs
	Created 10/2/2017 11:47:18 AM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;

namespace Utility
{
	public class ShakeTransform : MonoBehaviour 
	{
        private void OnEnable()
        {
            ShakeEvent.OnShakeEvent += Shake;
        }

        public void Shake()
        {
            DOTween.Shake(() => transform.position, x => transform.position = x, .5f, 10, 10, 45, true);
        }

        private void OnDisable()
        {
            ShakeEvent.OnShakeEvent -= Shake;
        }
    }
}