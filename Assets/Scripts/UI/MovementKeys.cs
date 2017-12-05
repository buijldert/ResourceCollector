/*
	MovementKeys.cs
	Created 10/3/2017 10:00:40 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace UI
{
	public class MovementKeys : MonoBehaviour 
	{
        public static bool IsDrilling;

        public void ChangeDrillingStatus(bool drilling)
        {
            IsDrilling = drilling;
        }
	}
}