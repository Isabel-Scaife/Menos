
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

    private Dictionary<uint, GameObject> vases;

    private Dictionary<int, GameObject> items;

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

    public void LoadVase(uint vaseID)
    {
        // load vase scene with vase
        SceneManager.LoadScene("PotPackage");
        Instantiate(vases[vaseID], Vector2.zero, Quaternion.identity);

        // remove vase id, will not spawn in the future 
        vases.Remove(vaseID); 
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}
