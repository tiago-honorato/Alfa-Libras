using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtility : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

    [Header("Configuração de Progresso")]
    public string[] categoriesToCheck;

    public void LoadSceneByProgression()
    {

        int finishedCount = 0;

        foreach (string categoryName in categoriesToCheck)
        {
            int status = DataSaver.ReadCategoryCurrentIndexValues(categoryName);

            Debug.Log(categoryName +"/"+status);

            if (status != -1)
            finishedCount += status;
        }

        switch(finishedCount)
        {
            case 5:
                SceneManager.LoadScene("SelectVideos");
                break;
            case 10:
                SceneManager.LoadScene("SelectVideos2");
                break;
            case 13:
                SceneManager.LoadScene("SelectVideos3");
                break;
            case 16:
                SceneManager.LoadScene("SelectVideos4");
                break;
            case 20:
                SceneManager.LoadScene("SelectVideos5");
                break;
            case 22:
                SceneManager.LoadScene("SelectVideos6");
                break;
            default:
                Debug.Log("ERRO! Scene não encontrada(GameUtility)");
                break;
        }

        Debug.Log("Contador: " + finishedCount);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void MuteToggleBackgroundMusic()
    {
        SoundManager.instance.ToggleBackgroundMusic();
    }

    public void MuteToggleSoundFX()
    {
        SoundManager.instance.ToggleSoundFX();
    }

}
