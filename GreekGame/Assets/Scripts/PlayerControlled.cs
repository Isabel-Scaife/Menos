using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum MoveDirections
{ 
    Forward, 
    Backward,
    Right,
    Left,
    None
}

public class PlayerControlled : MonoBehaviour
{
    // components
    protected Rigidbody2D rb;
    protected Animator animator;
    protected MoveDirections faceDirection = MoveDirections.Forward;

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
    protected bool controlBird = false;

    // guard detection
    public bool hidden = false;

    public Vector2 Direction { get { return direction; } }

    private void Awake()
    {
        // gets components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        // update player's position
        velocity = direction * speed;
        position = (Vector2)transform.position + velocity * Time.fixedDeltaTime;
        rb.MovePosition(position);

    }

    public virtual void Interact(InputAction.CallbackContext context)
    {
        // interact with item if something is within range
        if (context.performed)
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
        controlBird = !controlBird;

        if (CameraFollow.Instance == null) { Debug.Log("Missing camera follow"); return; }

        // swap follow target
        if (controlBird && this.CompareTag("Bird"))
        {
            CameraFollow.Instance.SetTarget(this.gameObject.transform);
        }
        else if (!controlBird && this.CompareTag("Player"))
        {
            CameraFollow.Instance.ResetCamera();
        }

    }

    /// <summary>
    /// Turns off player and bird input controls
    /// </summary>
    public void PauseInputControls()
    {
        playerInput.enabled = false;
    }

    /// <summary>
    /// gives control back to whoever had it before input was paused
    /// </summary>
    public void ResumeInputControls()
    {
        playerInput.enabled = true;
    }

    /// <summary>
    /// switches between movement/interaction input and input for advancing dialogue
    /// </summary>
    /// <param name="toDialogue">true if switching to input for advancing dialogue, 
    /// false if switching back to player movement</param>
    public void SwitchActionMaps(bool toDialogue)
    {
        // gets correct input component and map to switch to
        string targetMap = "Player";
        if (toDialogue) targetMap = "Dialogue";

        // switches current input to target map
        playerInput.SwitchCurrentActionMap(targetMap);
    }

    // advance dialogue on input
    public void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (context.performed && DialogueManager.Instance != null)
        {
            DialogueManager.Instance.Advance();
        }
    }

    public virtual void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if( direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.normalized.y))
            {
                if (direction.x >= 0)
                {
                    faceDirection = MoveDirections.Right;
                }
                else
                {
                    faceDirection= MoveDirections.Right;
                }
            }
            else
            {
                if (direction.y <= 0)
                {
                    faceDirection = MoveDirections.Forward;
                }
                else
                {
                    faceDirection = MoveDirections.Backward;
                }
            }

            // flip walk 
            if (direction.x > 0 && transform.localScale.x < 0 ||
                direction.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(
                    transform.localScale.x*-1,
                    transform.localScale.y,
                    transform.localScale.z);
            }
        }
        else
        {
            faceDirection = MoveDirections.None;
        }

        // play correct facing animation
        animator.SetInteger("direction", (int)faceDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // get reference to intertactable in rage
        Interactable script = collision.gameObject.GetComponent<Interactable>();
        if (script) interactObject = script;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactObject = null;
    }

    public void OpenJournalUI()
    {
        SceneManager.LoadScene("Journal");
    }
}

