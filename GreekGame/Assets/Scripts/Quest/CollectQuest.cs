using UnityEngine;
using System.Collections.Generic;

public class CollectQuest : MonoBehaviour, IQuestCompleter
{
    [SerializeField] private List<Item> itemsToCollect;
    [SerializeField] private int amountNeeded;
    private int amountCollected;

    [SerializeField] private List<string> questsID;
    public List<string> QuestsID { get => questsID; set => questsID = value; }


    private void OnEnable()
    {
        foreach(Item item in itemsToCollect)
        {
            item.OnCollect += IncreaseCount;
        }
    }

    private void OnDisable()
    {
        foreach (Item item in itemsToCollect)
        {
            item.OnCollect -= IncreaseCount;
        }
    }

    private void IncreaseCount()
    {
        amountCollected++;
        if(amountCollected >= amountNeeded)
        {
            ((IQuestCompleter)this).OnQuestComplete();
        }
    }
}
