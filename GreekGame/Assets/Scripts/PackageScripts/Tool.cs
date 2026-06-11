using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tool : MonoBehaviour
{
    [SerializeField]
    protected LayerMask clickable;

    protected bool mouseDown = false;
    protected FollowMouse mouse;

    private Vector3 startPosition;

    protected RaycastHit2D hit;

    private void Awake()
    {
        startPosition = transform.position;
        
        if((mouse = GetComponent<FollowMouse>()) == null)
        {
            Debug.Log(name + " is missing follow mouse component");
        }
    }

    /// <summary>
    /// Performs tools action 
    /// </summary>
    public virtual void Use() { }

    /// <summary>
    /// Resets any trackers for using if action complete or cancelled 
    /// </summary>
    public virtual void ResetUse() { }

    /// <summary>
    /// Updates current tool
    /// </summary>
    public virtual void SelectTool() 
    { 
        if (ToolManager.Instance == null)
        {
            Debug.LogError("No Tool Manager in scene");
        }
        else
        {
            mouse.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            ToolManager.Instance.CurrentTool = this;

            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = 100;
        }
    }

    /// <summary>
    /// Drop and reset tool 
    /// </summary>
    public virtual void DropTool()
    {
        if (ToolManager.Instance == null)
        {
            Debug.LogError("No Tool Manager in scene");
        }
        else
        {
            mouse.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Tool");
            ToolManager.Instance.CurrentTool = null;

            transform.position = startPosition;

            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = 10;
        }
    }
}
