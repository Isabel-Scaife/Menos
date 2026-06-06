using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerControlled
{

    private void Start()
    {
        // makes sure input control is only given to the player at start time
        SwitchActionMaps(false);

        // make sure SpawnManager exists
        if (SpawnManager.Instance == null)
        {
            Debug.Log("No SpawnManager in scene");
        }

        // set player's position to proper location 
        else
        {
            transform.position = SpawnManager.Instance.PlayerPosition;
        }
    }

    private void OnDestroy()
    {
        // some if statement to check if position needs to be stored should not be
        // stored when moving between jail and vineyard
        // like if the player touched a door they should spawn where the door tells 
        // them when they switch back 

        // make sure SpawnManager exists
        if (SpawnManager.Instance == null)
        {
            Debug.Log("No SpawnManager in scene");
        }

        // save the player's last position 
        else if (SpawnManager.Instance.SaveCurrentPosition)
        {
            SpawnManager.Instance.PlayerPosition = transform.position;
        }
    }
}
