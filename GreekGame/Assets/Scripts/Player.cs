using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerControlled
{

    public bool SpawnLastPosition { get; set; }

    private void Start()
    {
        // makes sure input control is only given to the player at start time
        SwitchActionMaps(false);

        // set player's position to proper location 
        transform.position = SpawnManager.Instance.PlayerPosition;
    }

    private void OnDestroy()
    {
        // some if statement to check if position needs to be stored should not be
        // stored when moving between jail and vineyard
        // like if the player touched a door they should spawn where the door tells 
        // them when they switch back 

        // save the player's last position 
        if(SpawnLastPosition)
        {
            SpawnManager.Instance.PlayerPosition = transform.position;
        }
    }
}
