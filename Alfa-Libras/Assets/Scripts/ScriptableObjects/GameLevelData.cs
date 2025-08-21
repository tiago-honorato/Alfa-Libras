using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "GameLevelData", menuName = "Scriptable Objects/GameLevelData")]
public class GameLevelData : ScriptableObject
{
    [System.Serializable]
    public struct CategoryRecord
    {
        public string categoryName;
        public List<BoardData> boardData;
    }

    public List<CategoryRecord> data;

}
