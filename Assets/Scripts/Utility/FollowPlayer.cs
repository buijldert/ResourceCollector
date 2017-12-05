/*
	FollowPlayer.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;

namespace Utility
{
    public class FollowPlayer : MonoBehaviour
    {
        //The transform of the player.
        [SerializeField]
        private Transform _player;

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
        }
    }
}