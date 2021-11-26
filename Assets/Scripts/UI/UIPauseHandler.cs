using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseHandler : MonoBehaviour
{
    GameObject pauseWindowUI;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseWindowUI = transform.Find("PauseWindow").gameObject;
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
        if (isPaused)
        {
            Unpause();
        }
    }

    public void DarkAreaClicked()
    {
        if (isPaused)
        {
            Unpause();
        }
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
        pauseWindowUI.SetActive(false);
        isPaused = false;
    }
}
