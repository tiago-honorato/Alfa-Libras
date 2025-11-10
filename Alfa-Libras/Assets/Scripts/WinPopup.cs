using UnityEngine;

public class WinPopup : MonoBehaviour
{

    public TextFader textFader1;
    public TextFader textFader2;
    public TextFader textFader3;

    public GameObject winPopup;

    void Start()
    {
        winPopup.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += ShowWinPopup;
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ShowWinPopup;
    }

    private void ShowWinPopup()
    {
        winPopup.SetActive(true);


        textFader1.FadeInOnly();
        textFader2.FadeInOnly();
        textFader3.FadeInOnly();


    }
    
    public void LoadNextLevel()
    {
        GameEvents.LoadNextLevelMethod();
        
    }   
}
