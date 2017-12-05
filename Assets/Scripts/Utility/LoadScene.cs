/*
	LoadScene.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class LoadScene : MonoBehaviour
    {
        /// <summary>
        /// Loads the scene with the given name.
        /// </summary>
        /// <param name="sceneName">The given name of the scene to load.</param>
        public void LoadLevel(string sceneName)
        {
            GameState.CGameState = CurrentGameState.Playing;
            SceneManager.LoadScene(sceneName);
        }
    }
}