using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SearchingWord : MonoBehaviour
{

    public TMP_Text displayedText;
    public Image crossLine;

    public TMP_FontAsset font;

    private string _word;

    void Start()
    {
        if (font != null)
        {
            displayedText.font = font;
        }
    }

    private void OnEnable()
    {

        GameEvents.OnCorrectWord += CorrectWord;

    }

    private void OnDisable()
    {

        GameEvents.OnCorrectWord -= CorrectWord;

    }

    public void SetWord(string word)
    {
        _word = word;
        displayedText.text = word;
    }

    private void CorrectWord(string word, List<int> squareIndexes)
    {

        if (word == _word)
        {

            crossLine.gameObject.SetActive(true);
            
        }

    }

}
