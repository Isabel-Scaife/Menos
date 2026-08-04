using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All dialogue involved in a certain interaction with an NPC
/// </summary>
[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/BasicDialogue")]
public class DialogueSO : ScriptableObject
{
    public string startingNodeID;
    public List<DialogueNode> nodes;
    public bool toggleFlagOnTalk;
    public string flag;
}
