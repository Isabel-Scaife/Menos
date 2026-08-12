using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class QuestObjectiveSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_objective;
    [SerializeField] private TextMeshProUGUI txt_tracking;

    public void RefreshObjective(QuestObjective objective)
    {
        txt_objective.text = objective.description;
        txt_tracking.text = $"{objective.currentAmount} / {objective.requiredAmount}";
    }

}
