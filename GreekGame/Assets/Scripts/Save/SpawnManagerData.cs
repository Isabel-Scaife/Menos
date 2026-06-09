using UnityEngine;

[System.Serializable]
public class SpawnManagerData : ISaveData<SpawnManager>
{

    public string[] items;

    public void CreateSaveData(SpawnManager spawnManager)
    { 
        items = new string[spawnManager.items.Count];
        spawnManager.items.CopyTo(items, 0);
    }
}
