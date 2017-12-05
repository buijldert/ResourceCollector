/*
	BombExplosion.cs
	Created 10/12/2017 9:21:32 AM
	Project Resource Collector by Base Games
*/
using Data;
using PlayerScripts;
using System.Collections;
using UnityEngine;
using Utility;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Interfaces;

namespace Assets
{
	public class BombExplosion : MonoBehaviour 
	{
        public delegate void OnDetonationAction(float healthMutation);
        public static event OnDetonationAction OnDetonation;

        [SerializeField] private float _bombDamage = 4;

        private CircleCollider2D _explosionRadius;
        private Harvester _harvester;
        private List<GameObject> _objectsToHarvest = new List<GameObject>();

        [SerializeField] private GameObject _explosionPrefab;

        private void Awake()
        {
            _explosionRadius = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == InlineStrings.RESOURCETAG)
            {
                if(collision.gameObject.name != "PavementSquare")
                {
                    _objectsToHarvest.Add(collision.gameObject);
                }
            }
            else if(collision.gameObject.tag == InlineStrings.PLAYERTAG)
            {
                if (OnDetonation != null)
                    OnDetonation(-_bombDamage);
            }else if(collision.gameObject.tag == InlineStrings.ENEMYTAG)
            {
                ExecuteEvents.Execute<IDamageable>(collision.gameObject, null, (x, y) => x.TakeDamage(-_bombDamage));
            }
        }

        private void OnEnable()
        {
            _harvester = GameObject.FindGameObjectWithTag(InlineStrings.PLAYERTAG).GetComponent<Harvester>();
            StartCoroutine(StartExplosionTimer());
        }

        private void OnDisable()
        {
            _objectsToHarvest.Clear(); 
        }

        IEnumerator StartExplosionTimer()
        {
            yield return new WaitForSeconds(2f);
            _explosionRadius.enabled = true;

            PlayExplosionSound();

            GameObject explosion = ObjectPool.Instance.GetObjectForType(_explosionPrefab.name, false);
            explosion.transform.position = transform.position;

            yield return new WaitForSeconds(0.05f);
            _explosionRadius.enabled = false;
            HarvestExplodedBlocks();
            ObjectPool.Instance.PoolObject(transform.parent.gameObject);
        }

        private void PlayExplosionSound()
        {
            if(DistanceToPlayer() >= 6f)
            {
                SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["ExplosionDistant"]);
            }
            else
            {
                SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["ExplosionClose"]);
            }
        }

        private float DistanceToPlayer()
        {
            Vector2 bombPosition = transform.position;
            Vector2 playerPos = GameObject.FindGameObjectWithTag(InlineStrings.PLAYERTAG).transform.position;

            float distance = Vector2.Distance(bombPosition, playerPos);

            return distance;
        }

        void HarvestExplodedBlocks()
        {
            //_harvester.HarvestResource(Directions.down, block, true, drill:false);
            _harvester.MultiHarvest(_objectsToHarvest);
        }
    }
}