using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds and manages flags and hidden stats that determine which dialogue happens, etc.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    // singleton
    public static GameStateManager Instance;

    // string flags for tracking game states
    private HashSet<string> flags = new HashSet<string>();

    // stats that determine which dialogue happens, etc.
    private const int NUMBEROFSTATS = 5;
    private int[] stats;

    void Awake()
    {
        // destroy duplicate instance
        if (Instance != null)
        {
            Debug.Log("Destroyed duplicate GameStateManager object");
            Destroy(this.gameObject);
            return;
        }

        // persistent singleton
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // default values for stats
        if (stats == null)
        {
            stats = new int[NUMBEROFSTATS];
        }
    }

    /// <summary>
    /// adds a flag to the list
    /// </summary>
    /// <param name="flag">flag to set</param>
    public void SetFlag(string flag)
    {
        flags.Add(flag);
    }

    /// <summary>
    /// checks if a flag is currently set
    /// </summary>
    /// <param name="flag">flag to check</param>
    /// <returns>true if the flag is in the list, false if not</returns>
    public bool HasFlag(string flag) => flags.Contains(flag);

    /// <summary>
    /// "unsets" a flag by removing it from the list
    /// </summary>
    /// <param name="flag">flag to clear</param>
    /// <returns>true if the flag was removed, false if it was not part of the list</returns>
    public bool ClearFlag(string flag) => flags.Remove(flag);

    /// <summary>
    /// get all current flags as an array
    /// </summary>
    /// <returns>a copy of all current flags</returns>
    public string[] GetFlags()
    {
        string[] flagsArray = new string[flags.Count];
        flags.CopyTo(flagsArray);
        return flagsArray;
    }
    /// <summary>
    /// gets all stat values as an array
    /// </summary>
    /// <returns>a copy of the manager's array of stats</returns>
    public int[] GetStats()
    {
        if (stats != null) return (int[])stats.Clone();
        else return new int[NUMBEROFSTATS];
    }

    /// <summary>
    /// adds the given values to each stat, respectively
    /// </summary>
    /// <param name="changes">values to add to each stat</param>
    public void ChangeStats(int[] changes)
    {
        // no changes if given array has different number of elements from internal stats array
        if (changes.Length != stats.Length) return;

        // adds each value to its corresponding stat
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] += changes[i];
        }
    }

    public void LoadData(GameStateManagerData data)
    {
        ChangeStats(data.stats);


        flags.Clear();
        foreach(string s in data.flags)
        {
            flags.Add(s);
        }
    }
}