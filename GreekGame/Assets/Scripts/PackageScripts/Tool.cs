using UnityEngine;
using UnityEngine.InputSystem;

public class Tool : MonoBehaviour
{

    private Vector2 mousePos;
    protected Vector2 worldPos;
    [SerializeField]
    protected bool mouseDown = false;
    private bool isFollowing = false;

    protected RaycastHit2D hit;

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
        Debug.Log("Pickuped Up tool");
        isFollowing = true;

        // change objects depth to be on top 
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        PackageManager.Instance.CurrentTool = this;
    }

    /// <summary>
    /// Updates the current tool in package manager
    /// </summary>
    public void DropTool()
    {
        Debug.Log("Dropped tool");
        isFollowing = false;

        // change objects depth to orginal value
        Vector3 pos = transform.position;
        pos.z = 1;
        transform.position = pos;

        PackageManager.Instance.CurrentTool = null;
    }

    /// <summary>
    /// Current tool follows mouse position
    /// </summary>
    private void Follow()
    {
        mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }
}
