using System.Collections.Generic;
using UnityEngine;

public class NPCCoworker : NPC, IQuestCompleter
{

    [SerializeField] private string[] prerequisiteQuestIDs;
    [SerializeField] private int[] lengthOfPrereq;


    [SerializeField] private List<string> questsID;
    public List<string> QuestsID { get => questsID; set => questsID = value; }


    public override void Interact(PlayerControlled player)
    {
        if(dialogues != null && dialogues.Count > 0)
        {
            if (DialogueManager.Instance == null) { Debug.Log("No DialogueManager in scene"); return; }

            // check if prerequiste quest met to run corresponding dialogue 
            if (prerequisiteQuestIDs == null || QuestManager.Instance == null) return;

            int index = 0;
            for (int i = 0; i < lengthOfPrereq.Length; i++)
            {
                int count = 0;
                bool allPrerequisitesComplete = true;

                // check if set of prerequists are all met
                while (count < lengthOfPrereq[i])
                {
                    allPrerequisitesComplete = QuestManager.Instance.IsQuestComplete(prerequisiteQuestIDs[index + count]);

                    // increase index to match start of next prerequiste location
                    if (!allPrerequisitesComplete) { index += lengthOfPrereq[i]; break; }
                    count++;
                }

                // does not run if one prerequiste not met
                if(allPrerequisitesComplete)
                {
                    DialogueManager.Instance.BeginDialogue(dialogues[i+1], player);
                    return;
                }
            }

            // run default dialogue
            ((IQuestCompleter)this).OnQuestComplete();
            DialogueManager.Instance.BeginDialogue(dialogues[0], player);
        }
    }

}
