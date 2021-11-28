using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class UIStoryVideoManager : MonoBehaviour
{
    [SerializeField] LevelChanger levelChanger;

    VideoPlayer storyVideo;

    // Start is called before the first frame update
    void Start()
    {
        storyVideo = GetComponent<VideoPlayer>();
        storyVideo.loopPointReached += GoToNextScene;
    }

    void GoToNextScene(VideoPlayer video)
    {
        levelChanger.ChangeTo(2);
    }
}
