using UnityEngine;
using UnityEngine.UIElements;

public class teleportMechanic : Interactable
{
    [SerializeField]
    Player player;

    [SerializeField]
    GameObject camera;

    //the other teleport door you want to spit the player out at
    [SerializeField]
    teleportMechanic teleportTo;

    //coordinates to paired door
    private Vector3 teleportToPos;

    //detects if player is in front of the door
    private bool inRange;

    //naming convention: Teleport_CurrentLocation_WhereIt'sGoing

    void Start()
    {
        //gets coordinates to teleport to
        teleportToPos = teleportTo.transform.position;
    }

    /// <summary>
    /// when player interacts with door
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

        //teleports both player and camera
         //must teleport both, otherwise camera goes all wonky and stops rendering
         //subtracts y so it teleports you slightly in front of the door
        camera.transform.position = new Vector3(teleportToPos.x, teleportToPos.y - 5, 0);
        player.transform.position = new Vector3(teleportToPos.x, teleportToPos.y - 5, 0);

    }
}
