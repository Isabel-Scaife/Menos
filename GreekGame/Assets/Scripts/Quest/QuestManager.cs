using System.Collections.Generic;
using UnityEngine;

public interface IEvent
{
    void OnQuestComplete();
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private HashSet<string> completedQuests = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Completes quest if not already complete
    /// </summary>
    /// <param name="quest"></param>
    public void CompleteQuest(QuestData quest)
    {
        // check if quest has already been completed
        if(!completedQuests.Contains(quest.QuestID))
        {
            completedQuests.Add(quest.QuestID);
            quest.QuestComplete();
        }
    }

    /// <summary>
    /// Determines whether given quest is complete
    /// </summary>
    /// <param name="questID"></param>
    /// <returns>true if complete, false otherwise</returns>
    public bool IsQuestComplete(string questID)
    {
        return completedQuests.Contains(questID);
    }
}
