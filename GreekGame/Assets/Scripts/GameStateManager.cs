using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // singleton
    public static GameStateManager Instance;

    // string flags for tracking game states
    private HashSet<string> flags = new HashSet<string>();

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
    }

    /// <summary>
    /// adds a flag to the list
    /// </summary>
    /// <param name="flag">flag to set</param>
    public void SetFlag(string flag) => flags.Add(flag);

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
}