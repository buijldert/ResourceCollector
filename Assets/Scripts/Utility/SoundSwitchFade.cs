/*
	SoundSwitchFade.cs
	Created 10/6/2017 10:58:27 AM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Utility
{
	public class SoundSwitchFade : MonoBehaviour
	{
        private static SoundSwitchFade s_Instance = null;

        public static SoundSwitchFade instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType(typeof(SoundSwitchFade)) as SoundSwitchFade;
                }

                return s_Instance;
            }

            set { }
        }

        private AudioSource _source;

        private void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
            }

            s_Instance = this;
            DontDestroyOnLoad(gameObject);

            _source = GetComponent<AudioSource>();
        }

        public IEnumerator FadeOut()
        {
            _source.DOFade(0f, 1f);
            yield return new WaitForSeconds(1f);
        }

        public IEnumerator FadeIn(AudioClip soundToFadeInto)
        {
            _source.clip = soundToFadeInto;
            _source.Play();
            _source.DOFade(1f, 1f);
            yield return new WaitForSeconds(1f);
        }
    }
}