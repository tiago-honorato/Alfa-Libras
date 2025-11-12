using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoDisplay; // Objeto que mostra o vídeo (RawImage, tela, etc.)

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    public void Play()
    {
        if (videoPlayer != null)
        {
            videoDisplay.SetActive(true);  // mostra o vídeo
            videoPlayer.Play();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoDisplay.SetActive(false); // esconde o vídeo quando termina
    }
}
