using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonHandler : MonoBehaviour
{
    [SerializeField] LevelChanger levelChanger;

    GameObject optionsWindowUI;
    GameObject areYouSureWindowUI;

    // Start is called before the first frame update
    void Start()
    {
        optionsWindowUI = transform.parent.Find("OptionsWindow").gameObject;
        areYouSureWindowUI = transform.parent.Find("AreYouSureWindow").gameObject;
    }
    
    public void PlayButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        levelChanger.ChangeTo(2);
    }

    public void OptionsButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        optionsWindowUI.SetActive(true);
    }

    public void OptionsOKButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        optionsWindowUI.SetActive(false);
    }

    public void OptionsDarkAreaClicked()
    {
        optionsWindowUI.SetActive(false);
    }

    public void QuitButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        areYouSureWindowUI.SetActive(true);
    }

    public void QuitYesButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            levelChanger.ChangeTo(-1);
        }
        else
        {
            Time.timeScale = 1;
            SoundManager.instance.ChangeMusic(SoundManager.calmTrack);
            levelChanger.ChangeTo(1);
        }
    }

    public void QuitNoButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1.2f);
        areYouSureWindowUI.SetActive(false);
    }

    public void QuitDarkAreaClicked()
    {
        areYouSureWindowUI.SetActive(false);
    }
}
