using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerControlled
{
    private void Start()
    {
        // makes sure input control is only given to the player at start time
        SwitchActionMaps(false);
    }
}
