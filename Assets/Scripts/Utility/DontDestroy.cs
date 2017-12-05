/*
	DontDestroy.cs
	Created 10/6/2017 10:34:50 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace Utility
{
	public class DontDestroy : MonoBehaviour 
	{
		void Awake()
        {
            DontDestroyOnLoad(this.gameObject); 
        }
	}
}