using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// optional data for a dialogue choice that affects the world when chosen 
/// (e.g. adds item to inventory, logs decision in journal, etc.)
/// </summary>
[System.Serializable]
public class DialogueOutcome : IQuestCompleter
{
    public List<string> flagsToSet;
    public int[] statChanges;
    public List<string> questsID;

    public List<string> QuestsID { get => questsID; set => questsID = value; }

}