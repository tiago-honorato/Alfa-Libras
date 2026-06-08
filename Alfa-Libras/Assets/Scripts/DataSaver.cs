using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static int ReadCategoryCurrentIndexValues(string name)
    {

        var value = 0;
        if (PlayerPrefs.HasKey(name))
            value = PlayerPrefs.GetInt(name);

        if (value == -1)
            value = 0;

        return value;

    }

    public static void SaveCategoryData(string categoryName, int currentIndex)
    {
        PlayerPrefs.SetInt(categoryName, currentIndex);
        PlayerPrefs.Save();
    }
    
    public static void ClearGameData(GameLevelData levelData)
    {
        foreach (var data in levelData.data)
        {
            PlayerPrefs.SetInt(data.categoryName, 0);
        }

        PlayerPrefs.Save();
    }
}
