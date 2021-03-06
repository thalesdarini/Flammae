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
        storyVideo.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Story.mp4");
        storyVideo.Play();
        storyVideo.loopPointReached += GoToNextScene;
        if (SoundManager.instance != null)
        {
            Destroy(SoundManager.instance.gameObject);
        }
    }

    public void GoToNextScene(VideoPlayer video)
    {
        levelChanger.ChangeTo(3);
    }
}
