/*
	QuitApplication.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;

namespace Utility
{
    public class QuitApplication : MonoBehaviour
    {
        /// <summary>
        /// Closes the application if possible.
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}