using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private string unlockFlag;      // flag that must be set for door to be opened
    
    public override void Interact(PlayerControlled player)
    {
        if(GameStateManager.Instance != null && GameStateManager.Instance.HasFlag(unlockFlag))
        {
            // open door
            transform.rotation = Quaternion.Euler(0, 90, 0);
            GetComponentInChildren<Collider2D>().enabled = false;
        }
        else
        {
            // play locked sound
        }
    }
}
