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
    private List<string> copyQuestID;
    private bool calledBefore = false;
    public List<string> QuestsID { get => QuestID(); set => questsID = value; }


    private List<string> QuestID()
    {
        if (calledBefore) return copyQuestID;
        else
        {
            calledBefore = true;
            
            copyQuestID = new List<string>();
            foreach(string id in questsID)
            {
                copyQuestID.Add(id);
            }

            return copyQuestID;
        }
    }



}