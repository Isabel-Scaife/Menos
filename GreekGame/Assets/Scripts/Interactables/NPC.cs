using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Person that can be talked to by the player when interacted with in the overworld
/// </summary>
public class NPC : Interactable
{
    // fields
    [SerializeField]
    protected List<DialogueSO> dialogues;
    [SerializeField] List<string> flagDialogueChecks; // higher index higher priority
    [SerializeField] List<int> flaggedDialogueIndex;
    
    public void ReplaceDialogue(DialogueSO newDialogue, int index)
    {
        if (index < dialogues.Count)
        {
            dialogues[index] = newDialogue;
        }
    }

    // for connecting NPCs to dialogue, storing save data and states, etc.
    // [SerializeField] protected string npcID;     // might need in the future

    /// <summary>
    /// Shows dialogue
    /// </summary>
    /// <param name="player">player interacting with this NPC</param>
    public override void Interact(PlayerControlled player)
    {
        // can be overridden if states determine which conversation should happen

        // shows dialogue (base class alwas runs first conversation)
        if (dialogues != null && dialogues.Count > 0)
        {
            if (DialogueManager.Instance == null) { Debug.Log("No DialogueManager in scene"); return; }

            // run default dialogue if there are no flags 
            if(flagDialogueChecks == null)
            {
                DialogueManager.Instance.BeginDialogue(dialogues[0], player);
                return;
            }

            if(GameStateManager.Instance == null) { Debug.Log("No Gamestate manager in scene"); return; }

            // check flags starting from highest index 
            for (int i = flagDialogueChecks.Count - 1; i >= 0; i--)
            {
                // flag exists run corresponding dialogue
                if (GameStateManager.Instance.HasFlag(flagDialogueChecks[i]))
                {
                    int dialogueNum = flaggedDialogueIndex[i];
                    DialogueManager.Instance.BeginDialogue(dialogues[dialogueNum], player);
                    return;
                }

            }

            // no flags set play default dialogue
            DialogueManager.Instance.BeginDialogue(dialogues[0], player);
        }
    }
}
