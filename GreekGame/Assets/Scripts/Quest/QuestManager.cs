using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IEvent
{
    void OnQuestComplete();
}

public interface IQuestCompleter
{
    public List<string> QuestsID { get; set; }

    /// <summary>
    /// Completes quest and remove from list, if it could be completed 
    /// </summary>
    public void OnQuestComplete()
    {
        if (QuestsID == null) return;
        
        if(QuestManager.Instance == null)
        {
            Debug.Log("Missing QuestManager");
            return;
        }

        // complete quest and remove from list, if possible 
        for (int i = QuestsID.Count - 1; i >= 0; i--)
        {
            if (QuestManager.Instance.CompleteQuest(QuestsID[i]))
            {
                QuestsID.RemoveAt(i);
            }
        }
    }
}


public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private HashSet<string> completedQuests = new HashSet<string>();
    private Dictionary<string, QuestData> allQuests = new Dictionary<string, QuestData>();

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Find all quests in scene
        QuestData[] questsInScene = FindObjectsByType<QuestData>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);

        // 2. add quests to all quest list, if any quest already in list end early
        foreach (QuestData quest in questsInScene)
        {
            if (allQuests.ContainsKey(quest.QuestID))
            {
                return;
            }
            else
            {
                allQuests.Add(quest.QuestID, quest);
            }

        }
    }


    /// <summary>
    /// Complete quest if requirments are met
    /// </summary>
    /// <param name="questID"></param>
    /// <returns>true if quest could be completed, false otherwise</returns>
    public bool CompleteQuest(string questID)
    {
        // quest exists and not already complete
        if (allQuests.ContainsKey(questID) && !completedQuests.Contains(questID))
        {
            // quest active and prerequisites met and 
            if(allQuests[questID].QuestComplete())
            {
                completedQuests.Add(questID);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Completes quest without checking if it is already complete or met prerequisites
    /// </summary>
    /// <param name="questID"></param>
    public void OverrideQuest(string questID)
    {
        if(allQuests.ContainsKey(questID))
        {
            completedQuests.Add(questID);
            allQuests[questID].OverrideComplete();
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
