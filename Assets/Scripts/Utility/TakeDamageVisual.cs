/*
	TakeDamageVisual.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections;
using UnityEngine;

namespace Utility
{
    public class TakeDamageVisual : MonoBehaviour
    {
        //The color the sprite will change to when damaged.
        [SerializeField]
        private Color32 _damageColor;
        //The normal color of the sprite.
        private Color32 _normalColor;

        //The spriterenderer attached to the gameobject of this script.
        private SpriteRenderer _sr;

        private Coroutine colorChangeCoroutine;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        protected void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _normalColor = _sr.color;
        }

        /// <summary>
        /// Changes the color when damaged.
        /// </summary>
        /// <param name="damage"></param>
        protected void ChangeColor(float damage)
        {
            colorChangeCoroutine = StartCoroutine(ColorChange());
        }

        /// <summary>
        /// Changes the color back to normal after a small delay.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ColorChange()
        {
            _sr.color = _damageColor;
            yield return new WaitForSeconds(.1f);
            _sr.color = _normalColor;
        }
    }
}