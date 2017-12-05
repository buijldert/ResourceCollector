/*
	Pickup.cs
	Created 10/25/2017 1:49:25 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using System.Collections.Generic;
using Environment;

namespace Utility
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private List<Block> _blocks = new List<Block>();
        [SerializeField] private List<Sprite> _possibleSprites = new List<Sprite>();
        private SpriteRenderer _renderer;
        private Block _pickupBlock;
        private List<float> _dropChances = new List<float>() { 40f, 25f, 15f, 10f, 7.5f, 2.5f };
        public Block PickupBlock { get { return _pickupBlock; } }

        private void OnEnable()
        {
            _renderer = GetComponent<SpriteRenderer>();

            int randomBlock = CalculateProbability(_dropChances);

            _pickupBlock = _blocks[randomBlock];
            _renderer.sprite = _possibleSprites[randomBlock];
        }

        private int CalculateProbability(List<float> probs)
        {
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Count; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Count - 1;
        }
    }
}