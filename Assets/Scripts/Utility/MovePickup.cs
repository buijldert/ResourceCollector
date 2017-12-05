/*
	CollectPickup.cs
	Created 10/26/2017 10:53:54 AM
	Project Resource Collector by Base Games
*/
using Data;
using UnityEngine;
using Environment;
using PlayerScripts;
using System.Collections.Generic;

namespace Utility
{
	public class MovePickup : MonoBehaviour 
	{
        public delegate void PickupAction(List<string> textList = null, string text = null);
        public static event PickupAction OnPickup;

        private HoverPickup _hoverPickup;
        private Transform _target;
        private bool _isMovingToTarget = false;

        private void OnEnable()
        {
            _hoverPickup = GetComponent<HoverPickup>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == InlineStrings.PLAYERTAG)
            {
                _target = collision.transform;
                _hoverPickup.KillHover();
            }
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        void MoveToTarget()
        {
            if(_target != null)
            {
                transform.position = Vector3.Lerp(transform.position, _target.position, 3f * Time.deltaTime);
                if(Vector2.Distance(transform.position, _target.position) < 0.5f)
                {
                    MoveCallBack();
                }
            }
        }

        private void MoveCallBack(GameObject itemToAdd = null)
        {
            _target = null;
            AddItemToInventory(GetComponent<Pickup>().PickupBlock);
            ObjectPool.Instance.PoolObject(gameObject);
        }

        private void AddItemToInventory(Block item)
        {
            Block resourceMined = new Block();
            if (PlayerInventory.CheckForDupes(item.BlockType))
            {
                PlayerInventory.GetCurrentMinedBlock(item).Stack += 1;
            }
            else
            {
                //set new the new item object's values
                resourceMined.BlockType = item.BlockType;
                resourceMined.SellValue = item.SellValue;
                resourceMined.Stack += 1;
                //add and sort the list on resource
                PlayerInventory.Items.Add(resourceMined);
                PlayerInventory.SortList();
            }

            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["PickupSound"], pitch: Random.Range(0.95f, 1.25f));

            if (OnPickup != null)
                OnPickup(text: " + 1 " + item.BlockType);
        }
    }
}