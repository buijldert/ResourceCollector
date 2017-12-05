/*
	UpgradeData.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UI;

namespace Data
{
    /// <summary>
    /// The types of upgrades that can be bought.
    /// </summary>
    public enum UpgradeTypes {Drill, FuelTank, Health }

    /// <summary>
    /// The data of the currently selected upgrade.
    /// </summary>
    public class UpgradeData
    {
        public static UpgradeTypes UpgradeType;
        public static int UpgradeLevel;
        public static int UpgradeCost;
        public static UpgradeButton UpgradedScript;
    }
}