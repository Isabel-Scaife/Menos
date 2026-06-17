using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifyQuest : MonoBehaviour, IEvent
{
    [SerializeField] private QuestData questToModify;
    [SerializeField] private List<string> prerequisiteQuestIDs;
    [SerializeField] private bool add; // true = add, false = remove

    public void OnQuestComplete()
    {
        if (questToModify != null && prerequisiteQuestIDs.Count != 0)
        {
            if (add)
            {
                foreach (string questID in prerequisiteQuestIDs)
                {
                    questToModify.AddPrerequisite(questID);
                }
            }
            else
            {
                foreach (string questID in prerequisiteQuestIDs)
                {
                    questToModify.AddPrerequisite(questID);
                }
            }
        }
    }
}
