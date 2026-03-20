using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single piece of dialogue or set of choices along with whatever comes next
/// </summary>
[System.Serializable]
public class DialogueNode
{
    public bool isEndpoint;
    public string id;
    public string nextNodeID;

    // either has text or choices, not both
    [TextArea(3, 6)] public string text;
    public List<DialogueChoice> choices;
}
