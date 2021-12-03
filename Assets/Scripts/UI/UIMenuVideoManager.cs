using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

[RequireComponent(typeof(VideoPlayer))]
public class UIMenuVideoManager : MonoBehaviour
{
    VideoPlayer menuVideo;

    // Start is called before the first frame update
    void Start()
    {
        menuVideo = GetComponent<VideoPlayer>();
        menuVideo.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Menu.mp4");
        menuVideo.Play();
    }
}
