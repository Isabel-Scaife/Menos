using UnityEditor.U2D.Tooling.Analyzer;
using UnityEngine;

// copy format when you have an object with advance variable types 
// aka vector2, Dictionary, etc
// need to get that data to simple types to save that is why constuctor
// takes in the type of data it converts

[System.Serializable]
public class PlayerData : ISaveData<Player>
{
    
    public float[] position;

    public void CreateSaveData(Player player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
