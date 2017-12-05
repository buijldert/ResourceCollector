/*
	ColorLerp.cs
	Created 9/28/2017 1:06:01 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;

namespace UI
{
	public class ColorLerp : MonoBehaviour 
	{
        [SerializeField]
        private Text _textToLerp;

        [SerializeField]
        private bool _startsLerping;

        private Color lerpedColor = Color.white;

        [SerializeField]
        public Color32 startColor = new Color32(66, 134, 244, 75);
        [SerializeField]
        public Color32 endColor = new Color32(10, 57, 132, 75);

        private Coroutine _lerpColorCoroutine;

        private void Start()
        {
            if (_startsLerping)
            {
                StartCoroutine(LerpColor());
            }
        }

        private IEnumerator LerpColor()
        {
            if(GameState.CGameState == CurrentGameState.Playing)
            {
                _textToLerp.enabled = true;
                lerpedColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
                _textToLerp.color = lerpedColor;
                yield return null;
                _lerpColorCoroutine = StartCoroutine(LerpColor());
            }
            else if(GameState.CGameState == CurrentGameState.Paused)
            {
                _textToLerp.enabled = false;
            }
        }

        public void ControlLerp(bool isLerping)
        {
            if (isLerping)
            {
                _lerpColorCoroutine = StartCoroutine(LerpColor());
            }
            else
            {
                if(_lerpColorCoroutine != null)
                    StopCoroutine(_lerpColorCoroutine);
            }
        }
    }
}