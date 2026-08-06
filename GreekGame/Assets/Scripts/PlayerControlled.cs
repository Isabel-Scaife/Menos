using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    [Header("Interactions")]
    [SerializeField] protected List<Interactable> interactables = new List<Interactable>();
    [SerializeField] protected Interactable currentInteractable = null;

    [Header("Movement")]
    [SerializeField] protected float speed;
    protected Vector2 direction;
    protected Vector2 velocity;
    protected Vector2 position;

    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private bool pauseMovement = false;
    [SerializeField] public bool controlBird = false;

    // guard detection
    public bool hidden = false;

    // helper
    protected bool ControllingThis
    {
        get {
            return (!controlBird && this.CompareTag("Player"))
                || (controlBird && this.CompareTag("Bird"));
        }
    }

    public Vector2 Direction { get { return direction; } }
    public bool PauseMovement { get => pauseMovement; set => pauseMovement = value; }

    protected virtual void Awake()
    {
        // gets components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        PauseMovement = false;
    }

    private void OnDisable()
    {
        PauseMovement = true;
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
        // interact with interactable in range
        if (context.performed && currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
    }

    public void SwapControlledObject()
    {
        if (PauseMovement) return;

        controlBird = !controlBird;

        if (CameraFollow.Instance == null) { Debug.Log("Missing camera follow"); return; }

        // swap follow target
        if (controlBird && this.CompareTag("Bird"))
        {
            CameraFollow.Instance.SetTarget(this.gameObject.transform);
            CameraFollow.Instance.SetDistance(9);
            CameraFollow.Instance.SetOffset(new Vector2(0, .3f));
        }
        else if (!controlBird && this.CompareTag("Player"))
        {
            CameraFollow.Instance.ResetCamera();
        }

        // apply sprite diection 
        direction = Vector2.zero;
        AnimateDirection();

        // change currently highlighted interactable
        if (currentInteractable != null)
        {
            currentInteractable.SetHighlight(ControllingThis);
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

    /// <summary>
    /// respawn player outside post office
    /// </summary>
    public async void Respawn(Vector3 respawnPosition)
    {
        PauseMovement = true;
        await ScreenFader.Instance.FadeOut();

        // set position
        this.transform.position = respawnPosition;

        await ScreenFader.Instance.FadeIn();
        PauseMovement = false;
        
    }

    public virtual void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        AnimateDirection();
    }

    private void AnimateDirection()
    {
        if (PauseMovement) direction = Vector2.zero;

        if (direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.normalized.y))
            {
                if (direction.x >= 0)  faceDirection = MoveDirections.Right;
                else faceDirection= MoveDirections.Right;
            }
            else
            {
                if (direction.y <= 0) faceDirection = MoveDirections.Forward;
                else faceDirection = MoveDirections.Backward;
            }

            // flip walk temporary 
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
        if (animator != null)
        {
            animator.SetInteger("direction", (int)faceDirection);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // get reference to intertactable in rage
        Interactable script = collision.GetComponent<Interactable>();
        if (script != null)
        {
            interactables.Add(script);

            // highlight this interactable if its the first one in range
            if (currentInteractable == null)
            {
                currentInteractable = script;
                if (ControllingThis) currentInteractable.SetHighlight(true);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        // remove interactable that goes out of range from options
        Interactable script = collision.GetComponent<Interactable>();
        if (script != null)
        {
            interactables.Remove(script);
            
            // highlight a different interactable in range if there is one
            if (currentInteractable == script)
            {
                if (ControllingThis) script.SetHighlight(false);
                if (interactables.Count > 0)
                {
                    currentInteractable = interactables[0];
                    if (ControllingThis) currentInteractable.SetHighlight(true);
                }
                else currentInteractable = null;
            }
        }
    }

    public void OpenJournalUI()
    {
        if (JournalManager.Instance == null) return;
        JournalManager.Instance.OpenJournal();
    }
}

