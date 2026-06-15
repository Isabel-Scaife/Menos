using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ProgressionManagerData : ISaveData<ProgressionManager>
{
    public float timePlayed;
    public string chapter;
    public string sceneName;
    public int collectables;

    public void CreateSaveData(ProgressionManager data)
    {
        timePlayed = data.TimePlayed;
        chapter = data.Chapter;
        sceneName = data.SceneName;
        collectables = data.Collectables;
    }

}
