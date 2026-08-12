using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject prefab_questLogSlot;

    [Header("Quest Information Section")]
    [SerializeField] private TextMeshProUGUI txt_questName;
    [SerializeField] private TextMeshProUGUI txt_questDescription;
    [SerializeField] private QuestObjectiveSlot[] objectiveSlots;

    private Dictionary<string, QuestLogSlot> questSlots;

    public void CreateNewSlot(QuestData quest)
    {
        if (questSlots == null)
        {
            questSlots = new Dictionary<string, QuestLogSlot>();
        }

        // create slot
        GameObject objectSlot = Instantiate(prefab_questLogSlot, this.transform);

        // add quest data and save to list
        QuestLogSlot newSlot = objectSlot.GetComponent<QuestLogSlot>();
        newSlot.SetQuest(quest);
        questSlots[quest.QuestID] = newSlot;

        // add on click event to slot 
        Button button = newSlot.btn_questButton;
        button.onClick.AddListener(() => OnQuestClicked(quest));
    }

    public void UpdateQuestSlot(string questID)
    {
        if (questSlots != null && questSlots.ContainsKey(questID))
        {
            questSlots[questID].UpdateStatus();
        }
    }

    public void OnQuestClicked(QuestData quest)
    {
        txt_questName.text = quest.questName;
        txt_questDescription.text = quest.questDescription;

        DisplayObjectives(quest);
    }

    /// <summary>
    /// Applies the objectives of the selected quest to the UI
    /// Toggles off extra objective slots
    /// </summary>
    /// <param name="quest"></param>
    public void DisplayObjectives(QuestData quest)
    {
        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            // identify number of objects to fill
            if (i < quest.questObjectives.Count)
            {
                QuestObjective currentObjective = quest.questObjectives[i];

                // apply data to slot
                objectiveSlots[i].gameObject.SetActive(true);
                objectiveSlots[i].RefreshObjective(currentObjective);
            }
            // toggle off extra objective slots
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
