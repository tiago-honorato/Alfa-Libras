using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "AlphabetData", menuName = "Scriptable Objects/AlphabetData")]
public class AlphabetData : ScriptableObject
{
    [System.Serializable]
    public class LettersData
    {
        public string letter;
        public Sprite image;
    }

    public List<LettersData> AlphabetPlain = new List<LettersData>();
    public List<LettersData> AlphabetNormal = new List<LettersData>();
    public List<LettersData> AlphabetHighlighted = new List<LettersData>();
    public List<LettersData> AlphabetWrong = new List<LettersData>();
}
