using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class HideBox : Interactable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Guard guard;
    [SerializeField]
    private PlayerControlled player;

    //KNOWN BUGS
    // always defaults to player movement after exiting box..? 
    //Sometimes won't switch states if ds
    //player is in view cone


    void Awake()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //This is a sloppy way to do it..
        //essentially since the interact button is taken
        //and when player input controls are paused,
        //there was no way to nest this in interact
        //or at least one i can think of
        //so for now, to enter box is E and to exit is SPACE

        //if player is alr hiding and needs to leave
        if (guard.playerDetectable == false && Input.GetKeyDown(KeyCode.Space))
        {
            //probably put a uncrouching sprite in here somewhere...?
            spriteRenderer.color = Color.pink;

            //renders the player detectable to guard
            guard.playerDetectable = true;

            //resumes input controls -- this always defaults back to player???
            player.ResumeInputControls();
        }
    }

    public override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

        //if player isn't currently hiding
        if (guard.playerDetectable == true && player is Player)
        {
            //probably put a crouching sprite in here somewhere...?
            spriteRenderer.color = Color.red;

            //renders the player undetectable to guard
            guard.playerDetectable = false;

            //pauses input control
            player.PauseInputControls();
        }
    }
}
