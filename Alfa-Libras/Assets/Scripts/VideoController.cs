using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoDisplay;
    public GameObject background;

    private string videoAtual;

    public void ReproduzirVideo(string urlVideo)
    {
        // Se clicar no mesmo vídeo novamente → fecha
        if (videoDisplay.activeSelf)
        {
            if (videoAtual == urlVideo)
            {
                FecharVideo();
                return;
            }
        }

        videoAtual = urlVideo;

        videoPlayer.Stop();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = urlVideo;

        background.SetActive(true);

        
        videoDisplay.SetActive(true);

        videoPlayer.Prepare();

        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        vp.prepareCompleted -= OnVideoPrepared;
        vp.Play();
    }

    public void FecharVideo()
    {
        videoPlayer.Stop();

        videoDisplay.SetActive(false);
        background.SetActive(false);

        videoAtual = null;
    }
}