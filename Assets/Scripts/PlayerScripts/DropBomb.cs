/*
	PlayerBombs.cs
	Created 10/11/2017 4:21:04 PM
	Project Resource Collector by Base Games
*/
using Data;
using System.Collections;
using UnityEngine;
using Utility;

namespace PlayerScripts
{
	public class DropBomb : MonoBehaviour 
	{
        [SerializeField]private GameObject _bomb;
        private bool _onCooldown;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                BombDrop();
            }
        }

        public void BombDrop()
        {
            if (GameState.CGameState != CurrentGameState.Paused)
            {
                StartCoroutine(DropTheBomb());
            }
        }

        IEnumerator DropTheBomb()
        {
            if(PlayerStats.AmountOfBombs > 0 && !_onCooldown)
            {
                _onCooldown = true;
                GameObject bomb = ObjectPool.Instance.GetObjectForType(_bomb.name, false);
                bomb.transform.position = transform.position;
                PlayerStats.AmountOfBombs -= 1;
                yield return new WaitForSeconds(0.25f);
                _onCooldown = false;
            }
        }
	}
}