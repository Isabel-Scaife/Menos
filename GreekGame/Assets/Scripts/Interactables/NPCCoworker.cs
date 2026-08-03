using System.Collections.Generic;
using UnityEngine;

public class NPCCoworker : NPC
{

    [SerializeField] private string[] prerequisiteQuestIDs;

    [SerializeField] private List<string> questsID;
    public List<string> QuestsID { get => questsID; set => questsID = value; }


    public override void Interact(PlayerControlled player)
    {
        if(dialogues != null && dialogues.Count > 0)
        {
            if (DialogueManager.Instance == null) { Debug.Log("No DialogueManager in scene"); return; }

            // check if prerequiste quest met to run corresponding dialogue 
            if (prerequisiteQuestIDs != null && QuestManager.Instance != null)
            {
                for (int i = 0; i < prerequisiteQuestIDs.Length; i++)
                {
                    if (QuestManager.Instance.IsQuestComplete(prerequisiteQuestIDs[i]))
                    {
                        DialogueManager.Instance.BeginDialogue(dialogues[i+1], player);
                        return;
                    }
                }
            }

            // run default dialogue
            DialogueManager.Instance.BeginDialogue(dialogues[0], player);
            ((IQuestCompleter)this).OnQuestComplete();
        }
    }

}
