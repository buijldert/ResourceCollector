/*
	DoTweenFade.cs
	Created 10/6/2017 11:13:32 AM
	Project Resource Collector by Base Games
*/
using DG.Tweening;
using UnityEngine;

namespace Environment
{
	public class DoTweenFade : MonoBehaviour 
	{
        private void OnEnable()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color32 spriteColor = sr.color;
            spriteColor.a = 255;
            sr.color = spriteColor;
            sr.DOFade(0f, 2f);
        }

        private void OnDisable()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
}