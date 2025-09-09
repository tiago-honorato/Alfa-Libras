using UnityEngine;

public class CreditsToggleButton : MonoBehaviour
{

    public GameObject CreditsPopup;
    public bool isCreditsPopupActive = false;

    public void ToggleCreditsPopup()
    {

        if (isCreditsPopupActive == false)
        {

            CreditsPopup.SetActive(true);
            isCreditsPopupActive = true;

        }
        else
        {

            CreditsPopup.SetActive(false);
            isCreditsPopupActive = false;

        }

    }
}
