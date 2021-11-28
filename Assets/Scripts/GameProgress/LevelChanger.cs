using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    Animator fadeAnimation;
    int levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        fadeAnimation = GetComponent<Animator>();
    }

    public void ChangeTo(int levelIndex)
    {
        levelToLoad = levelIndex;
        fadeAnimation.SetTrigger("LevelEnd");
    }

    public void FadeOutComplete()
    {
        if (levelToLoad != -1)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            Application.Quit();
        }
    }
}
