/*
	CollisionChecker.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using Data;
using Environment;
using PlayerScripts;

namespace Utility
{
    /// <summary>
    /// The direction that the player will be digging in.
    /// </summary>
    public enum Directions { right, left, down }

    public class CollisionChecker : MonoBehaviour
    {
        //The current collision gameobject.
        private GameObject _currentCollision;
        public GameObject CurrentCollision
        {
            get { return _currentCollision; }
        }

        //The current direction.
        [SerializeField] private Directions _direction;
        public Directions Direction
        {
            get { return _direction; }
        }

        //The harvester script for usage of its harvesting methods.
        private Harvester _harvester;
        
        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void Awake()
        {
            _harvester = GameObject.FindGameObjectWithTag(InlineStrings.PLAYERTAG).GetComponent<Harvester>();
        }

        /// <summary>
        /// Checks if there is a resource in the direction of the player.
        /// </summary>
        /// <param name="collision">The object that the collision checker is colliding with.</param>
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision != null && collision.gameObject.tag == "Resource")
            {
                _currentCollision = collision.gameObject;
                //start harvesting if the player is not already trying to harvest any other resource.
                if (!Harvester.IsHarvesting)
                {
                    _harvester.HarvestResource(_direction, collision.gameObject, false);
                }
            }
        }
    }
}