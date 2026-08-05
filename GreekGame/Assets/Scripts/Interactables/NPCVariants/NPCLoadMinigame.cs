using UnityEngine;

public class NPCLoadMinigame : NPCEvent
{
    [SerializeField] MinigameSwapper minigameSwapper;

    protected override void AddAfterDialogueEvent()
    {
        minigameSwapper.Interact(playerRef);
    }
    
}
