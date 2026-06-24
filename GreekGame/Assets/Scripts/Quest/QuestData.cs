using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    [SerializeField] private string questID;
    [SerializeField] private List<string> requiredPrerequisites; // quests needed to start this one

    // events that run when quest completed
    private List<IEvent> completionEvents = new List<IEvent>();

    public string QuestID { get => questID; set => questID=value; }

    private void Awake()
    {
        IEvent[] temp = this.GetComponents<IEvent>();
        foreach(IEvent e in temp)
        {
            completionEvents.Add(e);
        }
    }

    /// <summary>
    /// Completes quest if prerequisites met and active
    /// </summary>
    public bool QuestComplete()
    {
        if (AllPrerequisitesComplete() && this.enabled)
        { 
            foreach (IEvent currentEvent in completionEvents)
            {
                currentEvent.OnQuestComplete();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Completes quest without checking prerequisites
    /// </summary>
    public void OverrideComplete()
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
    /// Adds a new event upon quest completion
    /// </summary>
    /// <param name="newEvent"></param>
    public void AddEvent(IEvent newEvent)
    {
        completionEvents.Add(newEvent);
    }

    /// <summary>
    /// Removes event if found in list 
    /// </summary>
    /// <param name="newEvent"></param>
    public void RevomeEvent(IEvent newEvent)
    {
        completionEvents.Remove(newEvent);
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
