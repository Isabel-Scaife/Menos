using UnityEngine;
using UnityEngine.InputSystem;

public class Tool : MonoBehaviour
{
    private bool isFollowing = false;
    protected bool mouseDown = false;
    private Vector2 mousePos;
    protected Vector2 worldPos;

    private Vector3 startPosition;

    protected RaycastHit2D hit;

    private void Awake()
    {
        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        if(isFollowing)
        {
            Follow();
        }
    }

    /// <summary>
    /// Tools specific raycast check
    /// used to call actions methods 
    /// </summary>
    public virtual void RayCast() { }

    /// <summary>
    /// Performs tools designate action when the user clicks
    /// </summary>
    public virtual void Use() { }

    /// <summary>
    /// Resets any trackers for using if action complete or cancellted 
    /// </summary>
    public virtual void ResetUse() { }

    /// <summary>
    /// Updates the current tool in package manager
    /// </summary>
    public virtual void SelectTool() 
    {
        // start following mouse 
        isFollowing = true;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        PackageManager.Instance.CurrentTool = this;

        // change layer order
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 100;
    }

    /// <summary>
    /// Drops tool and resets it to its inital position
    /// </summary>
    public void DropTool()
    {
        // stop following 
        isFollowing = false;
        gameObject.layer = LayerMask.NameToLayer("Tool");
        PackageManager.Instance.CurrentTool = null;

        // reset position
        transform.position = startPosition;

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 10;

    }

    /// <summary>
    /// Current tool follows mouse
    /// </summary>
    private void Follow()
    {
        mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }
}
