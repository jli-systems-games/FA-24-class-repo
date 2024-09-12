using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class pretransition : MonoBehaviour
{

    public GameObject gameManagerScript;
    public VideoPlayer videoPlayer;

    [SerializeField] string videoFileName;

    public GameObject transitionCanvass;
    public GameObject gameCanvas;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if(videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }
    
    void EndReached(UnityEngine.Video.VideoPlayer vp)
     {
        gameCanvas.gameObject.SetActive(true);
        transitionCanvass.gameObject.SetActive(false);
     }
    
}
