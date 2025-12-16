using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtility : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

    public void LoadSceneByProgression(string categoryName)
    {

        int currentIndex = DataSaver.ReadCategoryCurrentIndexValues(categoryName);
        Debug.Log("valor atual: " + currentIndex);


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
