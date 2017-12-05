/*
	ConfirmDelete.cs
	Created 10/25/2017 10:41:22 AM
	Project Resource Collector by Base Games
*/
using Serialization;
using UnityEngine;

namespace Utility
{
	public class ConfirmDelete : MonoBehaviour 
	{
        [SerializeField] private GameObject _confirmDeleteScreen;
        [SerializeField] private LoadingScreen _loadingScreen;

		public void CheckSaveFile()
        {
            if(RemoveSaveFile.SaveFileExists)
            {
                _confirmDeleteScreen.SetActive(true);
            }
            else
            {
                _loadingScreen.Close("Main");
            }
        }
	}
}