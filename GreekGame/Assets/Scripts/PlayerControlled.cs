using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlled : MonoBehaviour
{
    // components
    protected Rigidbody2D rb;

    // interactions 
    [SerializeField]
    protected Interactable interactObject;

    // movement
    [SerializeField]
    protected float speed;
    protected Vector2 direction;
    protected Vector2 velocity;
    protected Vector2 position;

    // switching controllable object
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private PlayerInput birdInput;

    // storing what should be given control after cutscenes
    private bool birdControlledLast;

    private void Awake()
    {
        // gets components
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // update player's position
        velocity = direction * speed;
        position = (Vector2)transform.position + velocity * Time.fixedDeltaTime;
        rb.MovePosition(position);

    }

    public virtual void Interact(InputAction.CallbackContext context)
    {
        // interact with item if something is within range
        if (context.started)
        {
            // interacts with current interactable
            if (interactObject != null)
            {
                interactObject.Interact(this);
                //Debug.Log("Interaction Occurred");
            }
        }
    }

    public void SwapControlledObject()
    {
        // turn bird controls on 
        if(playerInput.inputIsActive)
        {
            playerInput.enabled = false;
            birdInput.enabled = true;
        }
        // turn player contorls on
        else
        {
            birdInput.enabled = false;
            playerInput.enabled = true;
        }
    }

    /// <summary>
    /// Turns off player and bird input controls
    /// </summary>
    public void PauseInputControls()
    {
        if (birdInput != null && birdInput.inputIsActive)
        {
            birdControlledLast = true;
            birdInput.enabled = false;
        }
        else
        {
            birdControlledLast = false;
            playerInput.enabled = false;
        }
    }

    /// <summary>
    /// gives control back to whoever had it before input was paused
    /// </summary>
    public void ResumeInputControls()
    {
        if (birdControlledLast) birdInput.enabled = true;
        else playerInput.enabled = true;
    }

    /// <summary>
    /// switches between movement/interaction input and input for advancing dialogue
    /// </summary>
    /// <param name="toDialogue">true if switching to input for advancing dialogue, 
    /// false if switching back to player movement</param>
    public void SwitchActionMaps(bool toDialogue)
    {
        // gets correct input component and map to switch to
        PlayerInput currInput = playerInput;
        if (birdInput.inputIsActive) currInput = birdInput;
        string targetMap = "Player";
        if (toDialogue) targetMap = "Dialogue";

        // switches current input to target map
        currInput.SwitchCurrentActionMap(targetMap);
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("running");
        // get reference to intertactable in rage
        interactObject = collision.gameObject.GetComponent<Interactable>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactObject = null;
    }
}

