/*
	Artifact.cs
	Created 9/29/2017 9:56:12 AM
	Project Resource Collector by Base Games
*/
using Data;
using UnityEngine;

namespace Environment
{
	public class Artifact : Block 
	{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == InlineStrings.COLLISIONCHECKERTAG)
            {
                if(PlayerStats.DrillLevel == 0)
                {
                    DrillRequirement = 0;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            DrillRequirement = 1000; 
        }
    }
}