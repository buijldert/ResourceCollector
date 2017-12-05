/*
	OpenShop.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Collections.Generic;
using DG.Tweening;
using Utility;

namespace UI
{
    public class OpenShop : MonoBehaviour
    {
        public delegate void ShopCloseAction();
        public static event ShopCloseAction OnShopClose;

        //The resources currently in the shop.
        [SerializeField] private ShopItems _shopItems;

        [SerializeField] private List<GameObject> _tabs = new List<GameObject>();

        //The shop screen gameobject.
        [SerializeField] private GameObject _shop;

        [SerializeField] private GameObject _standardTab;

        //The purchase screen.
        [FormerlySerializedAs("_purrrchaseScreen")]
        [SerializeField] private GameObject _purchaseScreen;
        //The button that appears when the player is close enought to the shop.
        [SerializeField] private GameObject _openShopButton;

        //A check if the shop can be opened.
        private bool _canOpenShop;

        /// <summary>
        /// Makes sure the shop can be opened when the player is in range.
        /// </summary>
        /// <param name="other">The object the shop collided with.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == InlineStrings.PLAYERTAG)
            {
                _canOpenShop = true;
            }
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (_canOpenShop)
            {
                _openShopButton.SetActive(true);
            }
            else
            {
                _openShopButton.SetActive(false);
            }
#if !MOBILE_INPUT
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (_canOpenShop && !_shop.activeInHierarchy && GameState.CGameState == CurrentGameState.Playing)
                {
                    OpenTheShop();
                }
                else if (_shop.activeInHierarchy && _canOpenShop)
                {
                    CloseTheShop();
                }
            }
#endif
        }

        /// <summary>
        /// Makes sure the shop can't open the shop when he is out of range.
        /// </summary>
        /// <param name="other">The object the shop collided with.</param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == InlineStrings.PLAYERTAG && _canOpenShop)
            {
                _canOpenShop = false;
            }
        }

        public void OpenTheShop()
        {
            _purchaseScreen.SetActive(false);
            _shop.SetActive(true);
            _shopItems.LoadItems();
        }

        public void CloseTheShop()
        {
            if (OnShopClose != null)
                OnShopClose();
            _shop.SetActive(false);
        }
    }
}