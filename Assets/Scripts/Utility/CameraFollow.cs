/*
	CameraFollow.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;

namespace Utility
{
    public class CameraFollow : MonoBehaviour
    {
        //The transform of the player.
        [SerializeField]
        private Transform _playerTransform;

        //The speed at which the camera will follow the player.
        private float _followSpeed = 3f;
        //The minimum x position of the camera.
        private float _cameraBoundXMin = 19f;
        //The maximum x position of the camera.
        private float _cameraBoundXMax = 20f;
        //The minimum y position of the camera.
        private float _cameraBoundYMin = 113.51f;
        //The maximum y position of the camera.
        private float _cameraBoundYMax = 3f;

        //The target position for the camera.
        private Vector3 target;

        /// <summary>
        /// Lerps the camera towards the target at _followSpeed speed.
        /// </summary>
        private void FixedUpdate()
        {
            target = new Vector3(Mathf.Clamp(_playerTransform.position.x, -_cameraBoundXMin, _cameraBoundXMax), Mathf.Clamp(_playerTransform.position.y, -_cameraBoundYMin, _cameraBoundYMax), transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, _followSpeed * Time.deltaTime);
        }
    }
}