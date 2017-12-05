/*
	BuyExplosives.cs
	Created 10/12/2017 11:22:51 AM
	Project Resource Collector by Base Games
*/
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class BuyExplosives : MonoBehaviour 
	{
        [SerializeField] private GameObject _purchaseScreen;
        [SerializeField]private Gold _gold;
        [SerializeField]private int _cost;
        [SerializeField]private Text _costText;

        private void OnEnable()
        {
            ShowCost();
        }

        void ShowCost()
        {
            _costText.text = "Buy an explosive for: " + _cost;
        }

        public void BuyExplosive()
        {
            if(PlayerStats.Gold >= _cost)
            {
                _gold.MutateGold(-_cost);
                PlayerStats.AmountOfBombs += 1;
                _purchaseScreen.SetActive(true);
            }
        }
    }
}