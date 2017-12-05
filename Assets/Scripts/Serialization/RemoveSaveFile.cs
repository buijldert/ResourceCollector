/*
	RemoveSaveFile.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using System.IO;
using UnityEngine;
using UnityEngine.UI;
using PlayerScripts;
using Data;

namespace Serialization
{
    public class RemoveSaveFile : MonoBehaviour
    {
        //The load button to be disabled if the savefile doesnt exist.
        [SerializeField] private Button _loadButton;

        //The name of the savefile.
        //[SerializeField]
        private string _fileName;
        //The path of the file.
        private string _filePath;

        public static bool SaveFileExists = false;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        private void OnEnable()
        {
            _fileName = InlineStrings.SAVEFILENAME;
            _filePath = Application.persistentDataPath + "/" + _fileName;
            if (!File.Exists(_filePath) && _loadButton != null)
            {
                SaveFileExists = false;
                _loadButton.interactable = false;
            }
            else
            {
                SaveFileExists = true;
            }
        }

        /// <summary>
        /// Deletes the savefile if it exists.
        /// </summary>
        public void DeleteFile()
        {
            PlayerInventory.ClearInventory();
            if (!File.Exists(_filePath))
            {
                Debug.Log("no " + _fileName + " file exists");
            }
            else
            {
                Debug.Log(_fileName + " file exists, deleting...");

                File.Delete(_filePath);
                LoadLevel.SaveFileExists = false;
            }
        }
    }
}