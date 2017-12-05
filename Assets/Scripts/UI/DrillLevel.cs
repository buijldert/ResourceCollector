/*
	DrillLevel.cs
	Created 10/10/2017 10:38:09 AM
	Project Resource Collector by Base Games
*/
using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class DrillLevel : MonoBehaviour 
	{
        [SerializeField]
        private List<GameObject> _drillLevelIndicators;

        private void Start()
        {
            ChangeDrillLevelIndication();
        }

        public void ChangeDrillLevelIndication()
        {
            for (int i = 0; i < _drillLevelIndicators.Count; i++)
            {
                _drillLevelIndicators[i].SetActive(false);
            }
            for (int i = 0; i < PlayerStats.DrillLevel; i++)
            {
                _drillLevelIndicators[i].SetActive(true);
            }
        }
    }
}