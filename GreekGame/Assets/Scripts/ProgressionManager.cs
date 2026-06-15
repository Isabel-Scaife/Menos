using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    // appears on save file 
    public float TimePlayed { get; private set; }
    public string Chapter { get; private set; }
    public string SceneName { get; private set; }
    public int Collectables { get; private set; }


    public static ProgressionManager Instance { get; private set; }

    void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadData(ProgressionManagerData data)
    {
        TimePlayed = data.timePlayed;
        Chapter = data.chapter;
        SceneName = data.sceneName;
        Collectables = data.collectables;
    }

    /// <summary>
    /// When changing scenes save scene name
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Starting Menu")
        {
            SceneName = scene.name;
        }
    }

    void OnEnable()
    {
         SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
