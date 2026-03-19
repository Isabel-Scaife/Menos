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

    // for connecting NPCs to dialogue, storing save data and states, etc.
    // might not be needed
    // [SerializeField] protected string npcID;     // might need in the future

    /// <summary>
    /// Shows dialogue
    /// </summary>
    /// <param name="player">player interacting with this NPC</param>
    public override void Interact(PlayerControlled player)
    {
        // can be overridden if states determine which dialogue should play

        // exits early if no interaction is allowed at this time
        if (!canInteract) return;

        // shows dialogue
        canInteract = false;
        if (dialogues != null && dialogues.Count > 0)
        {
            DialogueManager.Instance.BeginDialogue(dialogues[0]);
        }
    }
}
