/*
	PlayUISound.cs
	Created 10/5/2017 3:53:35 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using System.Collections;

namespace Utility
{
	public class PlayUISound : MonoBehaviour 
	{
		public void PlaySound()
        {
            StartCoroutine(WaitForSound());
        }

        IEnumerator WaitForSound()
        {
            AudioSource tempAS = gameObject.AddComponent<AudioSource>();
            tempAS.clip = SoundsDatabase.AudioClips["GeneralClickSound"];
            tempAS.volume = 0.1f;
            tempAS.Play();
            yield return new WaitForSeconds(tempAS.clip.length);
            Destroy(tempAS);
        }
	}
}