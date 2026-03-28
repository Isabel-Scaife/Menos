using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All dialogue involved in a certain interaction with an NPC
/// </summary>
[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public string startingNodeID;
    public List<DialogueNode> nodes;
}
