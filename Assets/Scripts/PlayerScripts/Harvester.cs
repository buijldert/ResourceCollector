/*
	Harvester.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using PlayerScripts;
using Utility;
using UI;
using Data;

namespace PlayerScripts
{
    public class Harvester : MonoBehaviour
    {
        public delegate void HarvestAction(Transform block);
        public static event HarvestAction OnHarvest;
        public delegate void HarvestActionText(List<string> texts = null, string text = null);
        public static event HarvestActionText OnHarvestText;
        public delegate void HarvestFuelReduction(float fuelMutation);
        public static event HarvestFuelReduction OnHarvestStart;

        private float _harvestCooldown = 0.1f;
        private bool _onCooldown;

        //collision checkers on each side of the player.
        [SerializeField] private CollisionChecker _collisionCheckerLeft;
        [SerializeField] private CollisionChecker _collisionCheckerRight;
        [SerializeField] private CollisionChecker _collisionCheckerDown;
        private AudioSource _source;

        public static bool IsHarvesting;
        private bool _canHarvest = true;

        [SerializeField]
        private BigLevelGenerator _bigLevelGenerator;

        [SerializeField]
        private PlayerMovement _playerMovement;

        private float _axisGate = 1.75f;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        IEnumerator StopSound()
        {
            SoundManager.instance.FadeSound(_source, fadeDuration:0.3f);
            yield return new WaitForSeconds(0.35f);
            _source.Stop();
            _source.volume = 1;
        }

        /// <summary>
        /// Start the process of harvesting your resource.
        /// </summary>
        /// <param name="direction">The direction which the player is facing.</param>
        /// <param name="objectToHarvest">Object to harvest(with which the player collided).</param>
        /// <param name="destroy">Destroy the resource if true.</param>
        public void HarvestResource(Directions direction, GameObject objectToHarvest, bool destroy, bool drill = true)
        {
            if (GroundChecker.IsGrounded && drill)
            {           
                    switch (direction)
                    {
                        case Directions.right:
                            if (_playerMovement.x > _axisGate)
                            {
                                //start harvesting if destroy is false.
                                if (!destroy) { StartCoroutine(StartHarvest(direction, objectToHarvest)); }
                                //destroy resource if the player was already harvesting this object.
                                else { SendSingleHarvestEvent(objectToHarvest); ObjectPool.Instance.PoolObject(objectToHarvest);}
                            }
                            break;
                        case Directions.left:
                            if (_playerMovement.x < -_axisGate)
                            {
                                if (!destroy) { StartCoroutine(StartHarvest(direction, objectToHarvest)); }
                                else { SendSingleHarvestEvent(objectToHarvest); ObjectPool.Instance.PoolObject(objectToHarvest); }
                            }
                            break;
                        case Directions.down:
                            if (_playerMovement.y < -_axisGate)
                            {
                                if (!destroy) { StartCoroutine(StartHarvest(direction, objectToHarvest)); }
                                else { SendSingleHarvestEvent(objectToHarvest); ObjectPool.Instance.PoolObject(objectToHarvest); }
                            }
                            break;
                    }
            }
         }

        public void MultiHarvest(List<GameObject> _objectsToHarvest)
        {

            for (int i = 0; i < _objectsToHarvest.Count; i++)
            {
                ObjectPool.Instance.PoolObject(_objectsToHarvest[i]);
            }
            MultipleHarvestEvents(_objectsToHarvest);
        }

        private void SendSingleHarvestEvent(GameObject block)
        {
            if (OnHarvest != null)
                OnHarvest(block.transform);

            Block blockToDestroy = block.GetComponent<Block>();

            if (OnHarvestText != null)
            {
                PlayBreakSound();
                if ((int)blockToDestroy.BlockType > (int)Block.BlockTypes.Stone)
                {
                        OnHarvestText(text: " + 1 " + blockToDestroy.BlockType);
                }
                AddItemToInventory(block);
            }

            switch(blockToDestroy.LevelType)
            {
                case Block.LevelTypes.TopLevel:
                    _bigLevelGenerator.TopLevel[blockToDestroy.ArrayPosition.x, blockToDestroy.ArrayPosition.y] = 10;
                    break;
                case Block.LevelTypes.MidLevel:
                    _bigLevelGenerator.MidLevel[blockToDestroy.ArrayPosition.y, blockToDestroy.ArrayPosition.x] = 10;
                    break;
                case Block.LevelTypes.BottomLevel:
                    _bigLevelGenerator.BottomLevel[blockToDestroy.ArrayPosition.x, blockToDestroy.ArrayPosition.y] = 10;
                    break;
            }
            PoolableBlocks.PoolableSquares.Remove(block);
        }

        private void MultipleHarvestEvents(List<GameObject> blocks)
        {
            List<Block> _blocks = new List<Block>();
            List<string> multipleTexts = new List<string>();

            for (int i = 0; i < blocks.Count; i++)
            {
                _blocks.Add(blocks[i].GetComponent<Block>());

                if ((int)_blocks[i].BlockType > (int)Block.BlockTypes.Stone)
                {
                    multipleTexts.Add(" + 1 " + _blocks[i].BlockType);
                }

                //if (OnHarvest != null)
                //    OnHarvest(blocks[i].transform);

                AddItemToInventory(blocks[i]);

                switch (_blocks[i].LevelType)
                {
                    case Block.LevelTypes.TopLevel:
                        _bigLevelGenerator.TopLevel[_blocks[i].ArrayPosition.x, _blocks[i].ArrayPosition.y] = 10;
                        break;
                    case Block.LevelTypes.MidLevel:
                        _bigLevelGenerator.MidLevel[_blocks[i].ArrayPosition.y, _blocks[i].ArrayPosition.x] = 10;
                        break;
                    case Block.LevelTypes.BottomLevel:
                        _bigLevelGenerator.BottomLevel[_blocks[i].ArrayPosition.x, _blocks[i].ArrayPosition.y] = 10;
                        break;
                }
                PoolableBlocks.PoolableSquares.Remove(blocks[i]);
            }

            PlayBreakSound();

            if (OnHarvestText != null)
            {
                OnHarvestText(texts: multipleTexts);
            }
        }

        void PlayDrillSound(float pitch)
        {
            _source.pitch = pitch;
            _source.Play();
        }

        IEnumerator StartHarvest(Directions direction, GameObject objectToHarvest)
        {
            if (PlayerStats.DrillLevel >= objectToHarvest.GetComponent<Block>().DrillRequirement)
            {
                if (GroundChecker.IsGrounded)
                {
                    _playerMovement._animator.MoveAnimation(true, "IsDrilling");

                    if (OnHarvestStart != null)
                        OnHarvestStart(-4f);

                    IsHarvesting = true;
                    PlayerMovement.CanMove = false;
                    _playerMovement.IsMoving = false;
                    PlayDrillSound(Random.Range(0.58f, 0.61f));
                    yield return new WaitForSeconds(0.85f);
                    GameObject currentCollision = CheckDirection(direction).CurrentCollision;

                    if (objectToHarvest != null && objectToHarvest == currentCollision)
                    {
                        StartCoroutine(CoolDown());
                        StartCoroutine(StopSound());
                        HarvestResource(direction, objectToHarvest, true);
                    }

                    _playerMovement._animator.MoveAnimation(false, "IsDrilling");

                    IsHarvesting = false;
                    //yield break;
                }
            }
            else
            {
                Block objectBlock = objectToHarvest.GetComponent<Block>();
                if (objectBlock.DrillRequirement > 100)
                {
                    OnHarvestText(text:"Undrillable");
                }else 
                    OnHarvestText(text:"Drill level " + objectBlock.DrillRequirement + " needed.");
            }
        }

        //search for the right collision checker
        private CollisionChecker CheckDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.right:
                    return _collisionCheckerRight;
                case Directions.left:
                    return _collisionCheckerLeft;
                case Directions.down:
                    return _collisionCheckerDown;
            }
            return null;
        }

        void PlayBreakSound()
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["BlockBreak"], pitch: Random.Range(0.7f, 1.3f));
        }

        //cooldown the harvesting. || fix to player harvesting 2 resource at once.
        IEnumerator CoolDown()
        {
            _onCooldown = true;
            yield return new WaitForSeconds(_harvestCooldown);
            PlayerMovement.CanMove = true;
            _onCooldown = false;
        }

        void AddItemToInventory(GameObject item)
        {
            Block resourceMined = new Block();
            Block itemStats = item.GetComponent<Block>();
            if (PlayerInventory.CheckForDupes(itemStats.BlockType))
            {
                PlayerInventory.GetCurrentMinedBlock(itemStats).Stack += 1;
                return;
            }
            else
            {
                if(itemStats.BlockType == Block.BlockTypes.Artifact)
                {
                    SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["ArtifactFound"], fadeOut: true);
                }
                //set new the new item object's values
                resourceMined.BlockType = itemStats.BlockType;
                resourceMined.SellValue = itemStats.SellValue;
                resourceMined.Stack += 1;
                //add and sort the list on resource
                PlayerInventory.Items.Add(resourceMined);
                PlayerInventory.SortList();
            }
        }
    }
}