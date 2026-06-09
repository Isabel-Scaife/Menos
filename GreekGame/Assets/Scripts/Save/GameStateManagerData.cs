using UnityEngine;

[System.Serializable]
public class GameStateManagerData : ISaveData<GameStateManager>
{
    public string[] flags;
    public int[] stats;

    public void CreateSaveData(GameStateManager gameStateManager)
    {
        flags = gameStateManager.GetFlags();

        stats = gameStateManager.GetStats();
    }
}
