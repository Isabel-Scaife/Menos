using System.Collections.Generic;
using UnityEngine;

public class QuestManagerData : ISaveData<QuestManager>
{
    public string[] completedQuests;

    public void CreateSaveData(QuestManager saveObject)
    {
        completedQuests = new string[saveObject.CompletedQuests.Count];
        saveObject.CompletedQuests.CopyTo(completedQuests, 0);
    }
}
