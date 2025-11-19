using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordChecker : MonoBehaviour
{

    public GameData currentGameData;
    public GameLevelData GameLevelData;

    private string _word;

    private int _assignedPoints = 0;
    private int _completedWords = 0;
    private Ray _rayUp, _rayDown;
    private Ray _rayLeft, _rayRight;
    private Ray _rayDiagonalLeftUp, _rayDiagonalLeftDown;
    private Ray _rayDiagonalRightUp, _rayDiagonalRightDown;
    private Ray _currentRay = new Ray();

    private Vector3 _rayStartPosition;
    private List<int> _correctSquareList = new List<int>();

    // --- ALTERAÇÃO: Variável para guardar a posição da última letra válida ---
    private Vector3 _lastSquarePosition;

    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
        GameEvents.OnLoadNextLevel += LoadNextGameLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
        GameEvents.OnLoadNextLevel -= LoadNextGameLevel;
    }

    private void LoadNextGameLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Start()
    {
        currentGameData.selectedBoardData.ClearData();
        _assignedPoints = 0;
        _completedWords = 0;
    }

    void Update()
    {
        if (_assignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(_rayUp.origin, _rayUp.direction * 4);
            // ... (seu código de debug original continua aqui se quiser manter)
        }
    }

    private void SquareSelected(string letter, Vector3 squarePosition, int SquareIndex)
    {
        if (_assignedPoints == 0)
        {
            _rayStartPosition = squarePosition;
            _correctSquareList.Add(SquareIndex);
            _word += letter;

            _rayUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, 1));
            _rayDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, -1));
            _rayLeft = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 0f));
            _rayRight = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 0f));
            _rayDiagonalLeftUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 1));
            _rayDiagonalLeftDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, -1));
            _rayDiagonalRightUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 1));
            _rayDiagonalRightDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, -1));

            // --- ALTERAÇÃO: Inicializa a última posição ---
            _lastSquarePosition = squarePosition;
        }
        else if (_assignedPoints == 1)
        {
            // --- ALTERAÇÃO: Verifica se pulou alguma letra entre a primeira e a segunda ---
            if (!IsAdjacent(_lastSquarePosition, squarePosition)) return;

            _correctSquareList.Add(SquareIndex);
            _currentRay = SelectRay(_rayStartPosition, squarePosition);
            GameEvents.SelectSquareMethod(squarePosition);
            _word += letter;

            // --- ALTERAÇÃO: Atualiza a última posição ---
            _lastSquarePosition = squarePosition;

            CheckWord();
        }
        else
        {
            if (IsPointOnTheRay(_currentRay, squarePosition))
            {
                // --- ALTERAÇÃO: Verifica se pulou letra entre a última e a atual ---
                if (!IsAdjacent(_lastSquarePosition, squarePosition)) return;

                _correctSquareList.Add(SquareIndex);
                GameEvents.SelectSquareMethod(squarePosition);
                _word += letter;

                // --- ALTERAÇÃO: Atualiza a última posição ---
                _lastSquarePosition = squarePosition;

                CheckWord();
            }
        }

        _assignedPoints++;
    }

    // --- ALTERAÇÃO: Nova função para verificar se existe um buraco na seleção ---
    private bool IsAdjacent(Vector3 startPosition, Vector3 endPosition)
    {
        var direction = (endPosition - startPosition).normalized;
        var distance = Vector3.Distance(startPosition, endPosition);

        // Lança um raio da letra anterior até a nova letra
        // Subtraímos um pouquinho da distância (0.1f) para garantir que não pegaremos o collider de trás se houver sobreposição
        var hits = Physics.RaycastAll(new Ray(startPosition, direction), distance);

        int validHits = 0;

        // Contamos quantos quadrados válidos o raio atingiu
        foreach (var hit in hits)
        {
            // Verifique se o objeto atingido tem o componente GridSquare (ou o nome do seu script de quadrado)
            if (hit.transform.GetComponent<GridSquare>() != null)
            {
                // Ignora o quadrado de onde o raio partiu (startPosition) se o Raycast o detectar
                if (Vector3.Distance(hit.transform.position, startPosition) > 0.01f)
                {
                    validHits++;
                }
            }
        }

        // Se atingiu mais de 1 quadrado (o destino + algum no meio), então pulou uma letra.
        // O esperado é atingir apenas 1 (o destino).
        return validHits <= 1;
    }

    private void CheckWord()
    {
        foreach (var searchingWord in currentGameData.selectedBoardData.SearchWords)
        {
            if (_word == searchingWord.Word && searchingWord.Found == false)
            {
                searchingWord.Found = true;
                GameEvents.CorrectWordMethod(_word, _correctSquareList);
                _completedWords++;
                _word = string.Empty;
                _correctSquareList.Clear();
                CheckBoardCompleted();
                return;
            }
        }
    }

    private bool IsPointOnTheRay(Ray currentRay, Vector3 point)
    {
        var hits = Physics.RaycastAll(currentRay, 100.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.position == point)
            {
                return true;
            }
        }
        return false;
    }

    private Ray SelectRay(Vector2 firstPosition, Vector2 secondPosition)
    {
        var direction = (secondPosition - firstPosition).normalized;
        float tolerance = 0.1f;

        if (Math.Abs(direction.x) < tolerance && Math.Abs(direction.y - 1f) < tolerance) return _rayUp;
        if (Math.Abs(direction.x) < tolerance && Math.Abs(direction.y - (-1f)) < tolerance) return _rayDown;
        if (Math.Abs(direction.x - (-1f)) < tolerance && Math.Abs(direction.y) < tolerance) return _rayLeft;
        if (Math.Abs(direction.x - 1f) < tolerance && Math.Abs(direction.y) < tolerance) return _rayRight;
        if (direction.x < 0f && direction.y > 0f) return _rayDiagonalLeftUp;
        if (direction.x < 0f && direction.y < 0f) return _rayDiagonalLeftDown;
        if (direction.x > 0f && direction.y > 0f) return _rayDiagonalRightUp;
        if (direction.x > 0f && direction.y < 0f) return _rayDiagonalRightDown;

        return _rayDown;
    }

    private void ClearSelection()
    {
        _assignedPoints = 0;
        _correctSquareList.Clear();
        _word = string.Empty;
    }

    private void CheckBoardCompleted()
    {
        // (Mantenha o código original do CheckBoardCompleted aqui, sem alterações)
        // ...
        bool loadNextCategory = false;

        if (currentGameData.selectedBoardData.SearchWords.Count == _completedWords)
        {
            var categoryName = currentGameData.selectedCategoryName;
            var currentBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(categoryName);
            var nextBoardIndex = -1;
            var currentCategoryIndex = 0;
            bool readNextLevelName = false;

            for (int index = 0; index < GameLevelData.data.Count; index++)
            {
                if (readNextLevelName)
                {
                    nextBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(GameLevelData.data[index].categoryName);
                    readNextLevelName = false;
                }

                if (GameLevelData.data[index].categoryName == categoryName)
                {
                    readNextLevelName = true;
                    currentCategoryIndex = index;
                }
            }
            var currentLevelSize = GameLevelData.data[currentCategoryIndex].boardData.Count;

            if (currentBoardIndex < currentLevelSize)
                currentBoardIndex += 1;

            DataSaver.SaveCategoryData(categoryName, currentBoardIndex);

            if (currentBoardIndex >= currentLevelSize)
            {
                currentCategoryIndex++;
                if (currentCategoryIndex < GameLevelData.data.Count)
                {
                    categoryName = GameLevelData.data[currentCategoryIndex].categoryName;
                    currentBoardIndex = 0;
                    loadNextCategory = true;

                    if (nextBoardIndex <= 0)
                    {
                        DataSaver.SaveCategoryData(categoryName, currentBoardIndex);
                    }
                }
                else
                {
                    SceneManager.LoadScene("SelectCategory");
                }
            }
            else
            {
                GameEvents.BoardCompletedMethod();
            }

            if (loadNextCategory)
                GameEvents.UnlockNextCategoryMethod();
        }
    }
}