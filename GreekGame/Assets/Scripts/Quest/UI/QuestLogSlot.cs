using UnityEngine;
using TMPro;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_questName;
    [SerializeField] private TextMeshProUGUI txt_questStatus;

    private QuestData currentQuest;

    public void SetQuest(QuestData quest)
    {
        currentQuest = quest;

        txt_questName.text = quest.questName;
        txt_questStatus.text = quest.status;
    }

    public void UpdateStatus()
    {
        txt_questStatus.text = currentQuest.status;
    }
}
