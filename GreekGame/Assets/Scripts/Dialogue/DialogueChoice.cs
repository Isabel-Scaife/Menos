using UnityEngine;

/// <summary>
/// A choice that the player can select during dialogue
/// </summary>
[System.Serializable]
public class DialogueChoice
{
    public string text;         // text in the selectable box
    public string nextNodeID;   // dialogue that comes next

    // optional data to affect the world when this choice is chosen (e.g. add item to inventory)
    public DialogueOutcome outcome;
}
