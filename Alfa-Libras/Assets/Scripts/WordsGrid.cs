using System.Collections.Generic;
using UnityEngine;

public class WordsGrid : MonoBehaviour
{

    public GameData currentGameData;
    public GameObject gridSquarePrefab;
    public AlphabetData alphabetData;

    public float squareOffSet = 0.0f;
    public float topPosition;

    private List<GameObject> _squareList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void SpawnGridSquares()
    {
        if (currentGameData != null)
        {

        }
    }

    private Vector3 GetSquareScale(Vector3 defaultScale)
    { 
    
        var finalScale = defaultScale;
        var adjustment = 0.01f;

        while (ShouldScaleDown(finalScale))
        {



        }
    }

    private bool ShouldScaleDown(Vector3 targetScale)
    { 
        var squareRect = gridSquarePrefab.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new Vector2(0f, 0f);
        var startPosition = new Vector2(0f, 0f);
        // TO DO
    }

}
