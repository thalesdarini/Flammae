using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseHandler : MonoBehaviour
{
    GameObject pauseWindowUI;
    GameObject optionsWindowUI;
    GameObject areYouSureWindowUI;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseWindowUI = transform.Find("PauseWindow").gameObject;
        optionsWindowUI = transform.Find("PauseWindow").Find("Window").Find("OptionsWindow").gameObject;
        areYouSureWindowUI = transform.Find("PauseWindow").Find("Window").Find("AreYouSureWindow").gameObject;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtonPressed();
        }
    }

    public void PauseButtonPressed()
    {
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    public void ResumeButtonPressed()
    {
        Unpause();
    }

    public void DarkAreaClicked()
    {
        Unpause();
    }

    void Pause()
    {
        Time.timeScale = 0;
        pauseWindowUI.SetActive(true);
        isPaused = true;
    }

    void Unpause()
    {
        Time.timeScale = 1;
        optionsWindowUI.SetActive(false);
        areYouSureWindowUI.SetActive(false);
        pauseWindowUI.SetActive(false);
        isPaused = false;
    }
}
