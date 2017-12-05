/*
	MuteButton.cs
	Created 10/2/2017 3:23:53 PM
	Project Resource Collector by Base Games
*/
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class VolumeManager : MonoBehaviour
	{
        //The muted and unmuted sprites.
        [SerializeField]
        private Sprite _muteSprite, _unmuteSprite;

        //The image displaying if the sound is muted or not.
        [SerializeField]
        private Image _soundImage;

        //The text that displays the volume level.
        [SerializeField]
        private Text _volumeText;

        //The slider representing the volume leve.
        [SerializeField]
        private Slider _volumeSlider;

        private void OnEnable()
        {
            SetVolume(PlayerPrefs.GetFloat(InlineStrings.VOLUME, 1f));
            if (AudioListener.volume > 0)
                _soundImage.sprite = _unmuteSprite;
            else
                _soundImage.sprite = _muteSprite;

            _volumeSlider.value = AudioListener.volume;
        }

        /// <summary>
        /// Changes the sound sprites and volume based on the button clicked and the sprite currently active.
        /// </summary>
		public void ChangeSoundStatus()
        {
            if(_soundImage.sprite == _muteSprite)
            {
                _soundImage.sprite = _unmuteSprite;
                SetVolume(1f);
            }
            else
            {
                _soundImage.sprite = _muteSprite;
                SetVolume(0f);
            }
            _volumeText.text = Mathf.RoundToInt(AudioListener.volume * 100f) + "/100";
            _volumeSlider.value = AudioListener.volume;
        }

        private void SetVolume(float volume)
        {
            AudioListener.volume = volume;
            PlayerPrefs.SetFloat(InlineStrings.VOLUME, volume);
        }

        /// <summary>
        /// Changes the volume to the slider value;
        /// </summary>
        public void ChangeVolume()
        {
            SetVolume(_volumeSlider.value);
            _volumeText.text = Mathf.RoundToInt(_volumeSlider.value * 100f).ToString() + "/100";
            if (AudioListener.volume > 0)
            {
                _soundImage.sprite = _unmuteSprite;
            }
            else
            {
                _soundImage.sprite = _muteSprite;
            }
        }
    }
}