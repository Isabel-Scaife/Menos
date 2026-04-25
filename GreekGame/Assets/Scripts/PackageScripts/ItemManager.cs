
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private uint[] vaseIds;
    [SerializeField]
    private GameObject[] vaseInstances;

    [SerializeField]
    private GameObject[] stampSets;

    private Dictionary<uint, GameObject> vases;

    private Dictionary<int, GameObject> items;

    private uint currentVaseId = 0;
    private uint currentStampSet = 0;
    public static ItemManager Instance { get; private set; }

    void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            // fill vase dictionary 
            vases = new Dictionary<uint, GameObject>();

            for(int i = 0; i < vaseIds.Length; i++)
            {
                vases.Add(vaseIds[i], vaseInstances[i]);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadVase(uint vaseId, uint stampSetIndex)
    {
        // load vase scene with vase
        SceneManager.LoadScene("PotPackage");
        currentVaseId = vaseId;
        currentStampSet = stampSetIndex;
    }

    /// <summary>
    /// Delegate to load vase Object
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "PotPackage")
        {
            // create stamp set
            Instantiate(stampSets[currentStampSet], Vector2.zero, Quaternion.identity);
            
            // create vase 
            Instantiate(vases[currentVaseId], Vector2.zero, Quaternion.identity);

            // remove vase id, will not spawn in the future 
            vases.Remove(currentVaseId);
        }
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}
