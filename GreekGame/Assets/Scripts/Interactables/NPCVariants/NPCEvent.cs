using UnityEngine;

public abstract class NPCEvent : NPC
{
    [Header("Event After Talking")]
    [SerializeField] private string eventFlag;
    protected PlayerControlled playerRef;

    public override void Interact(PlayerControlled player)
    {
        if (GameStateManager.Instance == null) { Debug.Log("No Gamestate manager in scene"); return; }

        if(GameStateManager.Instance.HasFlag(eventFlag))
        {
            playerRef = player;
            DialogueManager.Instance.OnDialogueEnd += AddAfterDialogueEvent;
        }
        base.Interact(player);
    }

    /// <summary>
    ///  Event that triggers after dialogue ends 
    /// </summary>
    protected abstract void AddAfterDialogueEvent();
}
