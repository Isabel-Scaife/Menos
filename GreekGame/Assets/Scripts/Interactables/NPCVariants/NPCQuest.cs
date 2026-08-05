using UnityEngine;
using System.Collections.Generic;

public class NPCQuest : NPC, IQuestCompleter
{
    [SerializeField] private List<string> questsID;

    public List<string> QuestsID { get => questsID; set => questsID = value; }

    /// <summary>
    /// Talk to NPC and complete quest 
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(PlayerControlled player)
    {
        ((IQuestCompleter)this).OnQuestComplete();
        base.Interact(player);
    }
}
