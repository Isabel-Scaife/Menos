using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Collect", menuName = "Quest/Type")]
public class CollectQuest : IQuestCompleter
{
    [SerializeField] private List<Item> itemsToCollect;
    [SerializeField] public int requiredAmount;
    [SerializeField] public int currentAmount;

    [SerializeField] private List<string> questsID;
    public List<string> QuestsID { get => questsID; set => questsID = value; }

    public void Begin()
    {
        currentAmount = 0;

        foreach (Item item in itemsToCollect)
        {
            item.OnCollect += IncreaseCount;
        }
    }

    private void IncreaseCount()
    {
        currentAmount++;
        if(currentAmount >= requiredAmount)
        {
            ((IQuestCompleter)this).OnQuestComplete();
        }
    }
}
