using UnityEngine;

public class NPCLoadMinigame : NPC
{
    [SerializeField] MinigameSwapper minigameSwapper;


    public override void Interact(PlayerControlled player)
    {
        base.Interact(player);

        minigameSwapper.Interact(player);
    }
    
}
