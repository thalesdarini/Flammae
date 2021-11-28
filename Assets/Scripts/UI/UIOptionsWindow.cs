using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsWindow : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundEffectVolumeSlider;

    private void OnEnable()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", .5f);
        soundEffectVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", .5f);
    }

    public void UpdateMusicVolume()
    {
        SoundManager.instance.UpdateMusicVolume(musicVolumeSlider.value);
    }

    public void UpdateSoundEffectsVolume()
    {
        SoundManager.instance.UpdateSoundEffectsVolume(soundEffectVolumeSlider.value);
    }
}
