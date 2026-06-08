
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<string> itemIDs;
    public HashSet<string> items = new HashSet<string>();

    public Vector3 PlayerPosition { get; set; }
    public bool SaveCurrentPosition { get; set; }

    [SerializeField]
    private GameObject[] stampSets;

    // used to load correct pairing for minigame
    private GameObject vasePrefab;
    private uint currentStampSet = 0;

    public static SpawnManager Instance { get; private set; }

    void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            // fill active item IDs
            foreach (string s in itemIDs)
            {
                if(!items.Add(s))
                {
                    Debug.LogError("Item ID #" + s + " already exists");
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Remove item so it will not spawn when scene loads in the future
    /// </summary>
    /// <param name="itemID">item to remove</param>
    public void RemoveItem(string itemID)
    {
        items.Remove(itemID);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadVase(string itemID, GameObject vaseGamePrefab, uint stampSetIndex)
    {
        SaveCurrentPosition = true;

        // remove item 
        items.Remove(itemID);

        // load vase scene with vase
        SceneManager.LoadScene("PotPackage");
        vasePrefab = vaseGamePrefab;
        currentStampSet = stampSetIndex;
    }

    /// <summary>
    /// Delegate to load vase Object
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // destroy items when loading scene 
        DestoryInactiveItems();

        // populate vase minigame
        if (scene.name == "PotPackage")
        {
            Instantiate(stampSets[currentStampSet], Vector2.zero, Quaternion.identity);
            Instantiate(vasePrefab, Vector2.zero, Quaternion.identity);
        }
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

    public void Save()
    {
        throw new NotImplementedException();
    }
}
