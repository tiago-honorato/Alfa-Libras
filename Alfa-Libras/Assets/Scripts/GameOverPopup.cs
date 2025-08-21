using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{

    public GameObject gameOverPopup;
    //public GameObject continuegameButton;

    void Start()
    {
        //continuegameButton.GetComponent<Button>().interactable = false;
        gameOverPopup.SetActive(false);

        GameEvents.OnGameOver += ShowGameOverPopup;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverPopup;
    }

    private void ShowGameOverPopup()
    {
        gameOverPopup.SetActive(true);
        //continuegameButton.GetComponent<Button>().interactable = false;
    }
}
