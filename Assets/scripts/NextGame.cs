using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class NextGame : MonoBehaviour
{

    public GameObject gameManagerScript;
    public VideoPlayer videoPlayer;
    public string[] games;
    public string randomGame;

    [SerializeField] string videoFileName;

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
        randomGame = games[Random.Range(0, games.Length)];
        SceneManager.LoadScene(randomGame);
     }
    
}