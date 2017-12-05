/*
	ClearPlayerPrefs.cs
	Created 10/25/2017 1:07:21 PM
	Project Resource Collector by Base Games
*/
using Data;
using Extensions;
using UnityEngine;

namespace Utility
{
	public class ClearPlayerPrefs : MonoBehaviour 
	{
        private void OnEnable()
        {
#if !UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs2.SetBool(InlineStrings.INSTRUCTIONSTATUS, true);
        }
	}
}