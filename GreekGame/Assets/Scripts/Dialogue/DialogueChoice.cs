using UnityEngine;

/// <summary>
/// A choice that the player can select during dialogue
/// </summary>
[System.Serializable]
public class DialogueChoice
{
    public string text;         // text in the selectable box
    public string nextNodeID;   // dialogue that comes next

    // also stores some outcome object that can be passed into a manager that
    // can then manipulate the rest of the world based on the player's choice,
    // such as adding an item to the player's inventory
}
