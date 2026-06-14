using System;
using UnityEngine;

public class SavePoint : Interactable
{
    /// <summary>
    /// Try to save game
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(PlayerControlled player)
    {
        try
        {
            if (player is Player)
            {
                SaveSystem.SaveData<Player, PlayerData>
                    ((Player)player, "/Player.json");
            }

            if (GameStateManager.Instance != null)
            {
                SaveSystem.SaveData<GameStateManager, GameStateManagerData>
                    (GameStateManager.Instance, "/GameStateManager.json");
            }

            if (SpawnManager.Instance != null)
            {
                SaveSystem.SaveData<SpawnManager, SpawnManagerData>
                    (SpawnManager.Instance, "/SpawnManager.json");
            }
               
            Debug.Log("Save Successful! Files found at: " + Application.persistentDataPath);
        }
        catch (Exception e)
        {
            throw new System.Exception("Error while saving.", e);
        }
    }
}
