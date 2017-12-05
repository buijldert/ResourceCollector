/*
	ControlUIBlur.cs
	Created 10/25/2017 10:25:01 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace UI
{
	public class ControlUIBlur : MonoBehaviour 
	{
        [SerializeField] private GameObject _UIBlur;

        private void OnEnable()
        {
            _UIBlur.SetActive(true);
        }

        private void OnDisable()
        {
            _UIBlur.SetActive(false);
        }
    }
}