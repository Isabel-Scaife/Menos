using UnityEngine;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject prefab_questLogSlot;
    
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
    }

    public void UpdateQuestSlot(string questID)
    {
        if (questSlots != null && questSlots.ContainsKey(questID))
        {
            questSlots[questID].UpdateStatus();
        }
    }
}
