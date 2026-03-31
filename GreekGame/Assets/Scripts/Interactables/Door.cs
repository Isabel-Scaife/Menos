
using UnityEngine;

public class Door : Interactable
{

    public override void Interact(PlayerControlled player)
    {
        if(!canInteract)
        {
            // sound failed to open

        }
        else
        {
            // open door
            transform.rotation = Quaternion.Euler(0, 90, 0);
            canInteract = false;
            GetComponentInChildren<Collider2D>().enabled = false;
        }
    }

    public void Unlock()
    {
        canInteract = true;
    }
}
