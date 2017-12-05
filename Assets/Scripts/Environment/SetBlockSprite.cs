/*
	SetBlockSprite.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class SetBlockSprite : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _possibleSprites;

        private void OnEnable()
        {
            GetComponent<SpriteRenderer>().sprite = _possibleSprites[Random.Range(0, _possibleSprites.Count)];
        }
    }
}