using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class SaveSystem
{
    /// <summary>
    /// Save a generic type if it has a correcsponding save option
    /// </summary>
    /// <typeparam name="T">type being saved (Player)</typeparam>
    /// <typeparam name="TData">type data class (PlayerData),
    /// requires ISaveData and constuctor</typeparam>
    /// <param name="saveObject">object being saved</param>
    /// <param name="saveFileID">folder</param>
    /// <param name="filename">json file to save to</param>
    public static void SaveData<T, TData>(T saveObject, string saveFileID, string filename) 
        where TData : ISaveData<T>, new()  
    {
        // create object data
        TData data = new TData();
        data.CreateSaveData(saveObject);

        // convert to json and save
        string json = JsonUtility.ToJson(data, true);

        // create folder if it does not exist
        string folderPath = Path.Combine(Application.persistentDataPath, saveFileID);
        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string path = Path.Combine(folderPath, filename);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Load generic data from a json file
    /// </summary>
    /// <typeparam name="TData">type data class (Playerdata)</typeparam>
    /// <param name="saveFileID">folder</param>
    /// <param name="filename">json file with data</param>
    /// <returns>generic data</returns>
    public static TData LoadData<TData>(string saveFileID, string filename)
        where TData : class
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileID, filename);
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

    /// <summary>
    /// Find Folders with save data and return metadata
    /// </summary>
    /// <returns>Dictionary using save file ID to find corresponding metadata</returns>
    public static Dictionary<string, ProgressionManagerData> ReadSaveFolderData()
    {
        Dictionary<string, ProgressionManagerData> saveFolderData = new Dictionary<string, ProgressionManagerData>();

        // find all save folders
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(Application.persistentDataPath).EnumerateDirectories();
        foreach (DirectoryInfo directoryInfo in directoryInfos) 
        {
            string saveFileID = directoryInfo.Name;

            // check if folder holds save data or something else
            string path = Path.Combine(Application.persistentDataPath, saveFileID, "ProgressionManager.json");
            if (!File.Exists(path))
            {
                Debug.Log("Skipped folder " +  saveFileID + " does not contain save data");
            }

            // add meta data to dictionary
            ProgressionManagerData data = LoadData<ProgressionManagerData>(saveFileID, "ProgressionManager.json");

            if(data != null)
            {
                saveFolderData.Add(saveFileID, data);
            }
            else
            {
                Debug.Log("Error when loading meta data. Save File ID: " + saveFileID);
            }
        }

        return saveFolderData;
    }
}
