using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public abstract class QuestEvent : ScriptableObject
{
    public GameObject targetObject;

    public abstract void PlayEvent();

    public void AddReference(GameObject anchor)
    {
        this.targetObject = anchor;
    }
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
            // this whole method needs to be removed 
        }
    }
}


public class QuestManager : MonoBehaviour
{
    private QuestLog currentQuestLog;

    public static QuestManager Instance { get; private set; }

    private HashSet<string> completedQuests = new HashSet<string>();
    private Dictionary<string, QuestData> allQuests = new Dictionary<string, QuestData>();

    public HashSet<string> CompletedQuests { get => completedQuests; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            completedQuests.Clear();
            allQuests.Clear();
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
        // find the quest log 
        currentQuestLog = FindFirstObjectByType<QuestLog>(FindObjectsInactive.Include);

        if(currentQuestLog == null)
        {
            Debug.Log("Quest Log not found");
        }
    }

    /// <summary>
    /// Adds new quest
    /// </summary>
    /// <param name="newQuest"></param>
    public void AddQuest(QuestData newQuest)
    {
        allQuests[newQuest.QuestID] = newQuest;
        currentQuestLog.CreateNewSlot(newQuest);
    }


    /// <summary>
    /// Add given quest to complete list
    /// </summary>
    /// <param name="questID">completed quest</param>
    public void CompleteQuest(string questID)
    {
        // quest exists and not already complete
        if (allQuests.ContainsKey(questID) && !completedQuests.Contains(questID))
        {
            completedQuests.Add(questID);
            
            // update quest log
            currentQuestLog.UpdateQuestSlot(questID);

            return;
        }

        Debug.Log("quest already completed");
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

    public void LoadData(QuestManagerData data)
    {
        completedQuests.Clear();

        foreach (string s in data.completedQuests)
        {
            if (!completedQuests.Add(s))
            {
                Debug.Log("Item ID #" + s + " already added");
            }
        }
    }
}
