/*
	PlayerPrefs2.cs
	Created 10/5/2017 11:36:43 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using System.Collections;

namespace Extensions
{
    public class PlayerPrefs2
    {
        public static void SetBool(string key, bool state)
        {
            PlayerPrefs.SetInt(key, state ? 1 : 0);
        }

        public static bool GetBool(string key)
        {
            int value = PlayerPrefs.GetInt(key);

            if (value == 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}