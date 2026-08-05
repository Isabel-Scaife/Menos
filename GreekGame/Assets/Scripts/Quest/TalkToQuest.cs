using System.Collections.Generic;
using UnityEngine;

public class TalkToQuest : MonoBehaviour, IQuestCompleter
{
    [SerializeField] private List<NPC> npcTalkTo;
    [SerializeField] private int amountNeeded;
    private int amountTalkedTo;

    [SerializeField] private List<string> questsID;
    public List<string> QuestsID { get => questsID; set => questsID = value; }

    private void OnEnable()
    {
        foreach (NPC npc in npcTalkTo)
        {
            npc.TalkedTo += IncreaseCount;
        }
    }

    private void OnDisable()
    {
        foreach (NPC npc in npcTalkTo)
        {
            npc.TalkedTo -= IncreaseCount;
        }
    }

    private void IncreaseCount(NPC npc)
    {
        // remove npc from list
        npcTalkTo.Remove(npc);
        npc.TalkedTo -=IncreaseCount;

        // increase
        amountTalkedTo++;
        Debug.Log(amountTalkedTo + "/" +  amountNeeded);
        if (amountTalkedTo >= amountNeeded)
        {
            ((IQuestCompleter)this).OnQuestComplete();
        }
    }
}
