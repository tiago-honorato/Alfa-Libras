using UnityEngine;

public class AlphabetToggle : MonoBehaviour
{

    public GameObject AlphabetPopup;
    public bool isAlphabetPopupActive = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ToggleAlphabetPopup()
    {

        if (isAlphabetPopupActive == false)
        {

            AlphabetPopup.SetActive(true);
            isAlphabetPopupActive = true;

        }
        else { 
        
            AlphabetPopup.SetActive(false);
            isAlphabetPopupActive = false;
        
        }

    }

}
