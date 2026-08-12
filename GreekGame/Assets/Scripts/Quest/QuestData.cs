using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class QuestData : MonoBehaviour
{
    [SerializeField] private string questID;

    [Header("Information displayed about Quest")]
    [SerializeField] public string questName;
    [SerializeField, TextArea] public string questDescription;
    public string status = "In Progress";

    [Header("Quest Parts")]
    [SerializeField] public List<QuestObjective> questObjectives;
    private int requiredAmount;
    private int currentAmount;

    [Header("Quest Complete Events")]
    [SerializeField] private List<QuestEvent> completionEvents;

    public string QuestID { get => questID; set => questID=value; }

    private void Awake()
    {
        // start all objectives
        foreach (QuestObjective objective in questObjectives)
        {
            Debug.Log("begin");
            objective.Begin();
            objective.Complete += ObjectiveCompleted;
        }
        
        requiredAmount = questObjectives.Count;
        currentAmount = 0;
    }

    /// <summary>
    /// When object turned on adds quest to quest list
    /// </summary>
    private void Start()
    {
        // create pop up 
        // TODO: 

        // add to quest manager
        if(QuestManager.Instance != null) QuestManager.Instance.AddQuest(this);
    }

    /// <summary>
    /// Increase complete objective amount, 
    /// when required amount met run completion events 
    /// </summary>
    private void ObjectiveCompleted()
    {
        // update quest status
        currentAmount++;
        status = "In progress";

        // complete quest if enough parts are complete
        if(currentAmount >= requiredAmount)
        {
            status = "Complete";
            QuestManager.Instance.CompleteQuest(questID);

            foreach (QuestEvent @event in completionEvents)
            {
                @event.PlayEvent();
            }
        }
    }

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
}


[System.Serializable]
public class QuestObjective
{
    public event Action Complete;

    public string description;
    public bool isComplete = false;

    [SerializeField] private UnityEngine.Object target;

    private Item targetItem;
    private NPC targetNPC;

    [SerializeField] public int requiredAmount;
    [SerializeField] public int currentAmount;

    /// <summary>
    /// Adds listeners to events so when 
    /// interacting with item increase current count
    /// </summary>
    public void Begin()
    {
        // convert object to correct type
        targetItem = ((GameObject)target).GetComponent<Item>();
        targetNPC = ((GameObject)target).GetComponent<NPC>();

        // add listener to object 
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
