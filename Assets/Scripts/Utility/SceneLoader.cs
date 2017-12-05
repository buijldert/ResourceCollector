/*
	SceneLoader.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Data;

namespace Utility
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Text _loadingText;

        [SerializeField]
        private Image _loadingBar;

        /// <summary>
        /// Loads the given scene with loadigbar
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(string scene)
        {
            StartCoroutine(LoadNewScene(scene));
            _loadingText.text = "Loading...";
            if(scene == "Main")
            {
                StartCoroutine(SoundSwitchFade.instance.FadeOut());
            }
        }

        private IEnumerator LoadNewScene(string scene)
        {
            StartCoroutine(SoundSwitchFade.instance.FadeOut());
            yield return new WaitForSeconds(0.75F);
            // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);

            // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
            while (!async.isDone)
            {
                _loadingBar.fillAmount = async.progress + 0.1f;
                yield return null;
            }
        }
    }
}
