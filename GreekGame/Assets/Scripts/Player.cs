using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PlayerControlled
{
    [SerializeField] public Bird bird;
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
    protected override void FixedUpdate()
    {
        if (!controlBird) base.FixedUpdate();
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

    public override void Move(InputAction.CallbackContext context)
    {
        if (!controlBird) base.Move(context);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // if colliding with bird's held item, immediately pick it up
        Item script = collision.GetComponent<Item>();
        if (script != null && script.HeldByBird)
        {
            script.Interact(this);
            return;
        }
        
        base.OnTriggerEnter2D(collision);
    }
}
