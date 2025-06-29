using UnityEngine;

public class WordChecker : MonoBehaviour
{

    public GameData currentGameData;

    private string _word;

    private void OnEnable()
    {

        GameEvents.OnCheckSquare += SquareSelected;

    }

    private void OnDisable()
    {

        GameEvents.OnCheckSquare -= SquareSelected;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SquareSelected(string letter, Vector3 squarePosition, int SquareIndex)
    {

        GameEvents.SelectSquareMethod(squarePosition);
        _word += letter;
        CheckWord();

    }

    private void CheckWord()
    {
        foreach (var searchingWord in currentGameData.selectedBoardData.SearchWords)
        {
            if(_word == searchingWord.Word)
            {
                _word = string.Empty;
                return;
            }
        }
    }
}
