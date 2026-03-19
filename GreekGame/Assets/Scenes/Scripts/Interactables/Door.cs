
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
            transform.rotation = Quaternion.Euler(0, 0, -90);

        }
    }

    public void Unlock()
    {
        canInteract = true;
    }
}
