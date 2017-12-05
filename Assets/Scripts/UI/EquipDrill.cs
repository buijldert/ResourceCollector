/*
	EquipDrill.cs
	Created 9/27/2017 2:46:53 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;
using Data;
using Utility;
using UnityEngine.EventSystems;

namespace UI
{
	public class EquipDrill : MonoBehaviour 
	{
        public delegate void DrillChangedAction();
        public static event DrillChangedAction OnDrillChanged;

        [SerializeField] private DrillLevel _drillLevel;

		public void ChangeDrill()
        {
            SoundManager.instance.PlaySound(SoundsDatabase.AudioClips["EquipSound"], volume: 0.05f);
            PlayerStats.DrillLevel = UpgradeData.UpgradeLevel;

            _drillLevel.ChangeDrillLevelIndication();

            if (OnDrillChanged != null)
                OnDrillChanged();

            UpgradeData.UpgradedScript.SetButtons();
        }
	}
}