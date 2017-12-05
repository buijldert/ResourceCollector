/*
	ControlScrollRect.cs
	Created 10/11/2017 10:49:08 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ControlScrollRect : MonoBehaviour 
	{
        private ScrollRect _scrollRect;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        void OnDisable()
        {
            _scrollRect.verticalNormalizedPosition = 1;
        }
	}
}