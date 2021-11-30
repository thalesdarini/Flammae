using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton Pattern
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    #endregion

    public static AudioClip calmTrack, fastTrack;
    public static AudioClip buff, buffStart, button, explosion, fireball, firewall, heal, healEnd, soul;
    public static float soundEffectsVolume;

    // referenciar componentes pelo Editor
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource soundEffectsAudioSource;

    [SerializeField] float zCameraPosition;

    public delegate void VolumePrefsUpdatedAction();
    public event VolumePrefsUpdatedAction SoundEffectsVolumePrefsUpdated;

    // Start is called before the first frame update
    void Start()
    {
        SetVolumeFromPlayerPrefs(); // inicializa volumes com 

        // Load tracks
        calmTrack = Resources.Load<AudioClip>("calmTrack");
        fastTrack = Resources.Load<AudioClip>("fastTrack");
        buff = Resources.Load<AudioClip>("buff");
        buffStart = Resources.Load<AudioClip>("buffStart"); 
        button = Resources.Load<AudioClip>("button");
        explosion = Resources.Load<AudioClip>("explosion");
        fireball = Resources.Load<AudioClip>("fireball");
        firewall = Resources.Load<AudioClip>("firewall");
        heal = Resources.Load<AudioClip>("heal"); 
        healEnd = Resources.Load<AudioClip>("healEnd"); 
        soul = Resources.Load<AudioClip>("soul");
    }

    public void ChangeMusic(AudioClip track)
    {
        musicAudioSource.clip = track;
        musicAudioSource.Play();
    }

    public void PlaySoundEffect(AudioClip audioClip, float volumeScale)
    {
        soundEffectsAudioSource.PlayOneShot(audioClip, volumeScale);
    }

    public void PlaySoundEffect(AudioClip audioClip, float volumeScale, Vector2 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, new Vector3(position.x, position.y, zCameraPosition), soundEffectsVolume * volumeScale);
    }

    public void UpdateMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        musicAudioSource.volume = volume / 4f;
    }

    public void UpdateSoundEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
        soundEffectsVolume = volume;
        soundEffectsAudioSource.volume = soundEffectsVolume;

        SoundEffectsVolumePrefsUpdated?.Invoke();
    }

    public void SetVolumeFromPlayerPrefs()
    {
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", .5f) / 4f;
        soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", .5f);
        soundEffectsAudioSource.volume = soundEffectsVolume;
    }
}