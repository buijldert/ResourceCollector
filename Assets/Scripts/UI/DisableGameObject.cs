/*
	DisableGameObject.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;

namespace UI
{
    public class DisableGameObject : MonoBehaviour
    {
        //The gameobject that will be disabled.
        [SerializeField]
        private GameObject _gameObjectToDisable;

        /// <summary>
        /// Disables the given gameobject.
        /// </summary>
        public void DisableGivenGameObject()
        {
            _gameObjectToDisable.SetActive(false);
        }
    }
}