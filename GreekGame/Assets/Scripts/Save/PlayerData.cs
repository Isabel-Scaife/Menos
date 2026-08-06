using UnityEngine;

// copy format when you have an object with advance variable types 
// aka vector2, Dictionary, etc
// need to get that data to simple types to save that is why constuctor
// takes in the type of data it converts

[System.Serializable]
public class PlayerData : ISaveData<Player>
{
    
    public Vector2 position;

    public void CreateSaveData(Player player)
    {
        position = player.transform.position;
    }
} 