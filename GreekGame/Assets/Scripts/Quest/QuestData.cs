using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/QuestData")]
public class QuestData : ScriptableObject
{
    [SerializeField] private string questID;
    [SerializeField] private List<string> requiredPrerequisites; // quests needed to start this one

    // events that run when quest completed
    List<IEvent> completionEvents = new List<IEvent>();

    public string QuestID { get => questID; set => questID=value; }

    public void QuestComplete()
    {
        foreach (IEvent currentEvent in completionEvents)
        {
            currentEvent.OnQuestComplete();
        }
    }

    /// <summary>
    /// Adds a new quest prerequisites
    /// </summary>
    /// <param name="questID"></param>
    public void AddPrerequisite(string questID)
    {
        requiredPrerequisites.Add(questID);
    }

    /// <summary>
    /// Removes quest prerequisite if found in list 
    /// </summary>
    /// <param name="questID"></param>
    public void RemovePrerequisite(string questID)
    {
        requiredPrerequisites.Remove(questID);
    }

    /// <summary>
    /// Check if all prerequisites are complete
    /// </summary>
    /// <returns>return true if all prerequisites are complete, false otherwise</returns>
    public bool AllPrerequisitesComplete()
    {
        // check if quest manager exists
        if (QuestManager.Instance == null)
        {
            Debug.Log("Missing quest manager");
            return false;
        }

        foreach (string questID in requiredPrerequisites)
        {
            if (!QuestManager.Instance.IsQuestComplete(questID))
            {
                return false;
            }
        }
        return true;
    }
}
