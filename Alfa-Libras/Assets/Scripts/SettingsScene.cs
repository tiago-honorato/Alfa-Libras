using UnityEngine;

public class SettingsScene : MonoBehaviour
{
    public GameLevelData levelData;

    public void ClearGameData()
    {
        
        DataSaver.ClearGameData(levelData);

    }

}
