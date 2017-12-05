/*
	Singleton.cs
	Created 10/9/2017 10:36:09 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace Utility
{
	public class Singleton : MonoBehaviour 
	{
        public static Singleton instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}