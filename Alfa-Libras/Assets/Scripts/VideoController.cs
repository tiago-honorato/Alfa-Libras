using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [Header("Componentes")]
    public VideoPlayer videoPlayer;
    public GameObject videoDisplay; // A tela onde o vídeo passa (RawImage ou Mesh)
    public GameObject background;   // O fundo escuro (opcional)

    public void ReproduzirVideo(VideoClip videoClicado)
    {
        // 1. Verifica se a tela já está ligada
        if (videoDisplay.activeSelf)
        {
            // Se o vídeo que está tocando é O MESMO que foi clicado...
            if (videoPlayer.clip == videoClicado)
            {
                // ...então o usuário quer FECHAR a tela (Toggle Off)
                FecharVideo();
                return; // Sai da função
            }
        }

        // 2. Se chegou aqui, ou a tela estava fechada, ou clicou em um vídeo diferente.
        // Então vamos trocar o vídeo e tocar.

        videoPlayer.clip = videoClicado; // Troca o "disco" do player

        background.SetActive(true);      // Liga o fundo
        videoDisplay.SetActive(true);    // Liga a tela
        videoPlayer.Play();              // Dá o play
    }

    public void FecharVideo()
    {
        videoPlayer.Stop();
        videoDisplay.SetActive(false);
        background.SetActive(false);

        // Limpa o clip para garantir que a lógica de comparação funcione bem na próxima
        videoPlayer.clip = null;
    }
}