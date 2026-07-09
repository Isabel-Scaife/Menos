using UnityEngine;

public class HideBox : Interactable
{
    [SerializeField]
    private Guard guard;
    [SerializeField]
    private PlayerControlled player;

    //KNOWN BUGS
    // always defaults to player movement after exiting box..? 
    //Sometimes won't switch states if ds
    //player is in view cone

    void Update()
    {
        //This is a sloppy way to do it..
        //essentially since the interact button is taken
        //and when player input controls are paused,
        //there was no way to nest this in interact
        //or at least one i can think of
        //so for now, to enter box is E and to exit is SPACE

        //if player is alr hiding and needs to leave
        if (player.hidden == true && Input.GetKeyDown(KeyCode.Space))
        {
            //probably put a uncrouching sprite in here somewhere...?

            //renders the player detectable to guard
            player.hidden = false;

            //resumes input controls -- this always defaults back to player???
            player.ResumeInputControls();
        }
    }

    public override void Interact(PlayerControlled player)
    {
        //if player isn't currently hiding
        if (player.hidden == false && player is Player)
        {
            //probably put a crouching sprite in here somewhere...?

            //renders the player undetectable to guard
            player.hidden = true;

            //pauses input control
            player.PauseInputControls();
        }
    }
}
