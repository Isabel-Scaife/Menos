using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public abstract class QuestEvent : ScriptableObject
{
    public GameObject gameObject;

    public abstract void PlayEvent();

    public void AddReference(GameObject gameObject)
    {
        this.gameObject = gameObject;
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
            // removed because quests no longer completed this way
            //if (QuestManager.Instance.CompleteQuest(QuestsID[i]))
            //{
            //    QuestsID.RemoveAt(i);
            //}
        }
    }
}


public class QuestManager : MonoBehaviour
{
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
    /// Add given quest to complete list
    /// </summary>
    /// <param name="questID">completed quest</param>
    public void CompleteQuest(string questID)
    {
        // quest exists and not already complete
        if (allQuests.ContainsKey(questID) && !completedQuests.Contains(questID))
        {
            completedQuests.Add(questID);
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
