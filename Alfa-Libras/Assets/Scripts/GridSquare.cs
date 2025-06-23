using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public int SquareIndex { get; set; }

    private AlphabetData.LettersData _normalLetterData;
    private AlphabetData.LettersData _selectedLetterData;
    private AlphabetData.LettersData _correctLetterData;

    private SpriteRenderer _displayedImage;

    void Start()
    {
        _displayedImage = GetComponent<SpriteRenderer>();
        //TO DO
    }

    public void SetSprite(AlphabetData.LettersData normalLetterData, AlphabetData.LettersData selectedLetterData, AlphabetData.LettersData correctLetterData)
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = selectedLetterData;
        _correctLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.image;

    }

}
