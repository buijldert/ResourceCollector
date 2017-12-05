/*
	GainedResource.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using UnityEngine.UI;
using Environment;
using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using Utility;

namespace UI
{
    public class FloatingText : MonoBehaviour
    {
        //The text that will float above the player.
        private Text _text;
        //The animation of the text.
        private Animation _animation;

        /// <summary>
        /// Use this for initializtion.
        /// </summary>
        private void OnEnable()
        {
            _text = GetComponent<Text>();
            _animation = GetComponent<Animation>();
            Harvester.OnHarvestText += StartQueue;
            MovePickup.OnPickup += StartQueue;
        }

        /// <summary>
        /// Use this for de - initialization.
        /// </summary>
        private void OnDisable()
        {
            Harvester.OnHarvestText -= StartQueue;
            MovePickup.OnPickup -= StartQueue;
        }

        /// <summary>
        /// Spawns a text component when a resource is picked up.
        /// </summary>
        /// <param name="blockType">The type of the block which text will show up.</param>
        private void PlayTextAnimation(string text)
        {
            _text.text = text;
            _animation.Play();
        }

        private void StartQueue(List<string> texts = null, string text = null)
        {
            StartCoroutine(QueueText(texts, text));
        }

        private IEnumerator QueueText(List<string> texts = null, string text = null)
        {
            if (texts != null){
                for (int i = 0; i < texts.Count; i++)
                {
                    PlayTextAnimation(texts[i]);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                PlayTextAnimation(text);
            }
        }
    }
}