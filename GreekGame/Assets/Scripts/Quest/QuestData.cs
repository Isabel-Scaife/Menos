using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestData : MonoBehaviour
{
    [SerializeField] private string questID;

    [Header("Information displayed about Quest")]
    [SerializeField] private string questName;
    [SerializeField] [TextArea] public string questDescription;

    [Header("Quest Parts")]
    [SerializeField] private List<QuestObjective> questObjectives;
    private int requiredAmount;
    private int currentAmount;

    [Header("Quest Complete Events")]
    [SerializeField] private List<QuestEvent> completionEvents;

    //// i do not think we need these anymore
    //[Header("Prerequisites for Quest")]
    //[SerializeField] private List<string> questPrerequisites; // quests needed to start this one
    //[SerializeField] private List<string> flagPrerequisites;

    public string QuestID { get => questID; set => questID=value; }

    private void Awake()
    {
        // start all objectives
        foreach (QuestObjective objective in questObjectives)
        {
            objective.Begin();
            objective.Complete += ObjectiveCompleted;
        }
        
        requiredAmount = questObjectives.Count;
        currentAmount = 0;
    }

    /// <summary>
    /// Increase complete objective amount, 
    /// when required amount met run completion events 
    /// </summary>
    private void ObjectiveCompleted()
    {
        currentAmount++;

        // complete quest if enough parts are complete
        if(currentAmount >= requiredAmount)
        {
            QuestManager.Instance.CompleteQuest(questID);

            foreach (QuestEvent @event in completionEvents)
            {
                @event.PlayEvent();
            }
        }
    }

    ///// <summary>
    ///// Adds a new quest prerequisites
    ///// </summary>
    ///// <param name="questID"></param>
    //public void AddPrerequisite(string questID)
    //{
    //    questPrerequisites.Add(questID);
    //}

    ///// <summary>
    ///// Removes quest prerequisite if found in list 
    ///// </summary>
    ///// <param name="questID"></param>
    //public void RemovePrerequisite(string questID)
    //{
    //    questPrerequisites.Remove(questID);
    //}

    /// <summary>
    /// Adds a new event upon quest completion
    /// </summary>
    /// <param name="newEvent"></param>
    public void AddEvent(QuestEvent newEvent)
    {
        completionEvents.Add(newEvent);
    }

    /// <summary>
    /// Removes event if found in list 
    /// </summary>
    /// <param name="newEvent"></param>
    public void RevomeEvent(QuestEvent newEvent)
    {
        completionEvents.Remove(newEvent);
    }

    //    /// <summary>
    //    /// Check if all prerequisites are complete
    //    /// </summary>
    //    /// <returns>return true if all prerequisites are complete, false otherwise</returns>
    //    public bool AllPrerequisitesComplete()
    //    {
    //        // check if quest manager exists
    //        if (QuestManager.Instance == null)
    //        {
    //            Debug.Log("Missing quest manager");
    //            return false;
    //        }

    //        // check quest prereqs
    //        foreach (string questID in questPrerequisites)
    //        {
    //            if (!QuestManager.Instance.IsQuestComplete(questID))
    //            {
    //                return false;
    //            }
    //        }

    //        // check flag prereqs
    //        if (GameStateManager.Instance != null) {
    //            foreach (string flag in flagPrerequisites)
    //            {
    //                if (!GameStateManager.Instance.HasFlag(flag))
    //                {
    //                    return false;
    //                }
    //            }
    //        }

    //        return true;
    //    }
}


[System.Serializable]
public class QuestObjective
{
    public event Action Complete;

    public string description;
    public bool isComplete = false;

    [SerializeField] private UnityEngine.Object target;

    public Item targetItem => target as Item;
    public NPC targetNPC => target as NPC;

    [SerializeField] public int requiredAmount;
    [SerializeField] public int currentAmount;

    /// <summary>
    /// Adds listeners to events so when 
    /// interacting with item increase current count
    /// </summary>
    public void Begin()
    {
        if (targetItem != null)
        {
            targetItem.OnCollect += IncreaseCount;
        }

        if (targetNPC != null)
        {
            targetNPC.TalkedTo += IncreaseCount;
        }
    }

    /// <summary>
    /// Increase current amount 
    /// </summary>
    private void IncreaseCount()
    {
        currentAmount++;
        if (currentAmount >= requiredAmount)
        {
            isComplete = true;
            
            if(Complete != null) Complete.Invoke();
        }
    }
}
