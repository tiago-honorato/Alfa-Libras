using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public int SquareIndex { get; set; }

    private AlphabetData.LettersData _normalLetterData;
    private AlphabetData.LettersData _selectedLetterData;
    private AlphabetData.LettersData _correctLetterData;

    private SpriteRenderer _displayedImage;

    private bool _selected;
    private bool _clicked;

    void Start()
    {
        _selected = false;
        _clicked = false;
        _displayedImage = GetComponent<SpriteRenderer>();

    }

    void OnEnable()
    {

        GameEvents.OnEnableSquareSelection += OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        GameEvents.OnSelectSquare += SelectSquare;

    }

    private void OnDisable()
    {
        GameEvents.OnEnableSquareSelection -= OnEnableSquareSelection;
        GameEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        GameEvents.OnSelectSquare -= SelectSquare;
    }

    public void OnEnableSquareSelection()
    {
        _clicked = true;
        _selected = false;
    }

    public void OnDisableSquareSelection()
    {
        _selected = false;
        _clicked = false;
    }

    private void SelectSquare(Vector3 position)
    {

        if (this.gameObject.transform.position == position)
            _displayedImage.sprite = _selectedLetterData.image;

    }

    public void SetSprite(AlphabetData.LettersData normalLetterData, AlphabetData.LettersData selectedLetterData, AlphabetData.LettersData correctLetterData)
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = selectedLetterData;
        _correctLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.image;

    }

    private void OnMouseDown()
    {

        OnEnableSquareSelection();
        GameEvents.EnableSquareSelectionMethod();


    }

}
