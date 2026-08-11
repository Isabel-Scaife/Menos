using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Person that can be talked to by the player when interacted with in the overworld
/// </summary>
public class NPC : Interactable
{
    public Action TalkedTo;

    // fields
    [SerializeField] protected List<DialogueSO> dialogues;

    [Header("Playing Flagged Dialogue")]
    [SerializeField] protected List<string> flagChecks; // higher index higher priority
    protected List<string[]> flagSets;

    [SerializeField] protected List<int> flagDialogueIndex;

    private void Start()
    {
        flagSets = new List<string[]>();

        for (int i = 0; i < flagChecks.Count; i++)
        {
            // seperate flag set by '," into individual strings
            // and place into new list
            string currentFlagSet = flagChecks[i];
            flagSets.Add(currentFlagSet.Split(','));
        }
    }

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

            if (TalkedTo != null) TalkedTo.Invoke();

            // run default dialogue if there are no flags 
            if (flagSets == null)
            {
                DialogueManager.Instance.BeginDialogue(dialogues[0], player);
                return;
            }

            // found and ran flag dialgoue 
            if (CheckFlagDialogue(player)) return;

            // no flags set play default dialogue
            DialogueManager.Instance.BeginDialogue(dialogues[0], player);
        }
    }

    /// <summary>
    /// Checks dialogue flags and runs corresponding dialogue
    /// </summary>
    /// <returns>
    /// Return true if dialogue begins,
    /// false if dialogue does not begin
    /// </returns>
    protected bool CheckFlagDialogue(PlayerControlled player)
    {
        if (GameStateManager.Instance == null) { Debug.Log("No Gamestate manager in scene"); return false; }

        // check rows of prerequisites at a time
        for (int i = flagSets.Count - 1; i >= 0; i--)
        {
            bool allPrereqMet = true;

            foreach (string flag in flagSets[i])
            {
                // break early in prerequisite not met 
                if (!GameStateManager.Instance.HasFlag(flag))
                {
                    allPrereqMet = false;
                    break;
                }
            }

            // if all prerquisites met run dilogue
            if (allPrereqMet)
            {
                // check for out of bounds index
                if (i >= flagDialogueIndex.Count)
                {
                    Debug.Log($"NPC {this.name}: has missing dialogue index for flag prerequisite set");
                    return false;
                }

                // run dialogue
                int index = flagDialogueIndex[i];
                DialogueManager.Instance.BeginDialogue(dialogues[index], player);
                return true;
            }
        }
        return false;
    }
}
