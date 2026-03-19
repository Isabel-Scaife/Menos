using UnityEngine;

public class HideBox : Interactable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Guard guard;

    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

        //---KNOWN BUGS---
        //Input controls don't pause
        //Sometimes won't switch states if 
        //player is in view cone

        //if player isn't currently hiding
        if (guard.playerDetectable == true)
        {
            //probably put a crouching sprite in here somewhere...?
            spriteRenderer.color = Color.red;

            //renders the player undetectable to guard
            guard.playerDetectable = false;

            //pauses input controls -- this doesn't work :(
            player.PauseInputControls();
        }
        //if player is hiding and needs to leave
        else
        {
            //probably put a uncrouching sprite in here somewhere...?
            spriteRenderer.color = Color.pink;

            //renders the player detectable to guard
            guard.playerDetectable = true;

            //resumes input controls -- this doesn't work :(
            player.ResumeInputControls();
        }
    }
}
