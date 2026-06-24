using UnityEngine;

[System.Serializable]
public class SpawnManagerData : ISaveData<SpawnManager>
{

    public string[] items;
    public string[] pastScenesLoaded;
    public Vector2 playerPosition;

    public void CreateSaveData(SpawnManager spawnManager)
    {
        items = new string[spawnManager.Items.Count];
        spawnManager.Items.CopyTo(items, 0);

        pastScenesLoaded = new string[spawnManager.PastScenesLoaded.Count];
        spawnManager.PastScenesLoaded.CopyTo(pastScenesLoaded, 0);

        playerPosition = spawnManager.PlayerPosition;
    }

}