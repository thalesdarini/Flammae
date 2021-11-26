using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonHandler : MonoBehaviour
{
    GameObject optionsWindowUI;
    GameObject areYouSureWindowUI;

    // Start is called before the first frame update
    void Start()
    {
        optionsWindowUI = transform.parent.Find("OptionsWindow").gameObject;
        areYouSureWindowUI = transform.parent.Find("AreYouSureWindow").gameObject;
    }

    public void OptionsButtonPressed()
    {
        optionsWindowUI.SetActive(true);
    }

    public void OptionsOKButtonPressed()
    {
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
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("Menu");
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
