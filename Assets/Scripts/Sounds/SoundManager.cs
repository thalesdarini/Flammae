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
    public static AudioClip infernal_fall, infernal_bite, player_death, cast, summoning, dash, slash;
    public static AudioClip battlecry, bowShoot, buff, buffStart, button, explosion, fireball, firewall, heal, healEnd, soul, swordAttack;
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
        battlecry = Resources.Load<AudioClip>("battlecry");
        bowShoot = Resources.Load<AudioClip>("bowShoot");
        buff = Resources.Load<AudioClip>("buff");
        buffStart = Resources.Load<AudioClip>("buffStart"); 
        button = Resources.Load<AudioClip>("button");
        calmTrack = Resources.Load<AudioClip>("calmTrack");
        explosion = Resources.Load<AudioClip>("explosion");
        fastTrack = Resources.Load<AudioClip>("fastTrack");
        fireball = Resources.Load<AudioClip>("fireball");
        firewall = Resources.Load<AudioClip>("firewall");
        heal = Resources.Load<AudioClip>("heal"); 
        healEnd = Resources.Load<AudioClip>("healEnd"); 
        soul = Resources.Load<AudioClip>("soul");
        swordAttack = Resources.Load<AudioClip>("swordAttack");
        //player sounds
        cast = Resources.Load<AudioClip>("cast");
        slash = Resources.Load<AudioClip>("slash");
        dash = Resources.Load<AudioClip>("dash");
        summoning = Resources.Load<AudioClip>("summoning");
        player_death = Resources.Load<AudioClip>("player_death");
        //infernal sounds
        infernal_bite = Resources.Load<AudioClip>("infernal_bite");
        infernal_fall = Resources.Load<AudioClip>("infernal_fall");
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