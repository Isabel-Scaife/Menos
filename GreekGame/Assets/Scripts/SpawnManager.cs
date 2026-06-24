
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{

    private HashSet<string> items = new HashSet<string>();

    // track every scene that has been loaded 
    private HashSet<string> pastScenesLoaded = new HashSet<string>();

    public static SpawnManager Instance { get; private set; }
    public Vector3 PlayerPosition { get; set; }
    public bool SaveCurrentPosition { get; set; }
    public HashSet<string> Items { get => items; }
    public HashSet<string> PastScenesLoaded { get => pastScenesLoaded; }

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
    /// <summary>
    /// Check if the given scene has been loaded before
    /// </summary>
    /// <returns>true if scene has been load in the past, false otherwise</returns>
    public bool SceneLoadedInPast(string scene)
    {
        return pastScenesLoaded.Contains(scene);
    }


    /// <summary>
    /// Remove item so it will not spawn when scene loads in the future
    /// </summary>
    /// <param name="itemID">item to remove</param>
    public void RemoveItem(string itemID)
    {
        items.Remove(itemID);
    }

    /// <summary>
    /// Delegate to load vase Object
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        // add items in scene if first time it has been encountered
        if (!pastScenesLoaded.Contains(scene.name))
        {
            AddItems();
            pastScenesLoaded.Add(scene.name);

        }
        else
        {
            // destroy items when loading scene (second time onward)
            DestoryInactiveItems();
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

    /// <summary>
    /// Destory items not found in active list 
    /// </summary>
    private void DestoryInactiveItems()
    {
        // 1.Find items in loaded scene
        Item[] itemsInScene = FindObjectsByType<Item>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);

        foreach (Item item in itemsInScene)
        {
            // 2. If item is NOT in active list delete item
            if (!items.Contains(item.ItemID))
            {
                Destroy(item.gameObject);
            }
        }
    }

    /// <summary>
    /// Add all items found in scene to items list 
    /// </summary>
    private void AddItems()
    {
        Item[] itemsInScene = FindObjectsByType<Item>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);

        foreach (Item item in itemsInScene)
        {
            if (!items.Add(item.ItemID))
            {
                Debug.LogError("Item " + item.name + " has an already existing id, will not add to list.");
            }

        }
    }

    public void LoadData(SpawnManagerData data)
    {
        items.Clear();

        foreach (string s in data.items)
        {
            if (!items.Add(s))
            {
                Debug.Log("Item ID #" + s + " already added");
            }
        }

        pastScenesLoaded.Clear();

        foreach (string s in data.pastScenesLoaded)
        {
            if (!pastScenesLoaded.Add(s))
            {
                Debug.Log("Scene " + s + " already added");
            }
        }

        PlayerPosition = data.playerPosition;
    }
}
