using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
 
public class VideoPlayerController2 : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("Lvl2");
    }
}
