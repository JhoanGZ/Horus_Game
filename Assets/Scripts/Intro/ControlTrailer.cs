using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ControlTrailer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip introClip;  // Intro Pg18 Video
    public VideoClip mainClip;   // Principal Trailer
    public string nextSceneName;

    void Start()
    {
        videoPlayer.clip = introClip;  // Start first video
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnIntroEnd;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //For Skip scene
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void OnIntroEnd(VideoPlayer vp)
    {
        // Start second video on reach first video loop
        videoPlayer.clip = mainClip;
        videoPlayer.Play();
        videoPlayer.loopPointReached -= OnIntroEnd;
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu");
    }
}