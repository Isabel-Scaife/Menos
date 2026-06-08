using UnityEngine;
using System.IO;

public static class SaveSystem
{
    /// <summary>
    /// Save a generic type if it has a correcsponding save option
    /// </summary>
    /// <typeparam name="T">type being saved (Player)</typeparam>
    /// <typeparam name="TData">type data class (PlayerData),
    /// requires ISaveData and constuctor</typeparam>
    /// <param name="saveObject">object being saved</param>
    /// <param name="filename">json file to save to</param>
    public static void SaveData<T, TData>(T saveObject, string filename) 
        where TData : ISaveData<T>, new()  
    {
        // create object data
        TData data = new TData();
        data.CreateSaveData(saveObject);

        // convert to json and save
        string json = JsonUtility.ToJson(data, true);
        string path = Application.persistentDataPath + filename;
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Load generic data from a json file
    /// </summary>
    /// <typeparam name="TData">type data class (Playerdata)</typeparam>
    /// <param name="filename">json file with data</param>
    /// <returns>generic data</returns>
    public static TData LoadData<TData>(string filename)
        where TData : class
    {
        string path = Application.persistentDataPath + filename;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
           TData data = JsonUtility.FromJson<TData>(json);
            
           return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
