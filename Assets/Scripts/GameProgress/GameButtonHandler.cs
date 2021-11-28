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
        levelChanger.ChangeTo(1);
    }

    public void OptionsButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1);
        optionsWindowUI.SetActive(true);
    }

    public void OptionsOKButtonPressed()
    {
        SoundManager.instance.PlaySoundEffect(SoundManager.button, 1);
        optionsWindowUI.SetActive(false);
    }

    public void OptionsDarkAreaClicked()
    {
        optionsWindowUI.SetActive(false);
    }

    public void QuitButtonPressed()
    {
        areYouSureWindowUI.SetActive(true);
    }

    public void QuitYesButtonPressed()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            levelChanger.ChangeTo(-1);
        }
        else
        {
            Time.timeScale = 1;
            levelChanger.ChangeTo(0);
        }
    }

    public void QuitNoButtonPressed()
    {
        areYouSureWindowUI.SetActive(false);
    }

    public void QuitDarkAreaClicked()
    {
        areYouSureWindowUI.SetActive(false);
    }
}
