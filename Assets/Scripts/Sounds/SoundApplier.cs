using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundApplier : MonoBehaviour
{
    [SerializeField] float volumeScale;
    AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        UpdateSoundEffectVolume();
        soundEffect.spatialBlend = 1f;
        if (soundEffect.clip != null)
        {
            soundEffect.Play();
        }

        SoundManager.instance.SoundEffectsVolumePrefsUpdated += UpdateSoundEffectVolume;
    }

    void UpdateSoundEffectVolume()
    {
        if (soundEffect != null)
        {
            soundEffect.volume = SoundManager.soundEffectsVolume * volumeScale;
        }
    }
}
