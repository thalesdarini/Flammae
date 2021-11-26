using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    Animator fadeAnimation;
    string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        fadeAnimation = GetComponent<Animator>();
    }

    public void ChangeToLevel(string levelName)
    {
        levelToLoad = levelName;
        fadeAnimation.SetTrigger("LevelEnd");
    }

    public void FadeOutComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
