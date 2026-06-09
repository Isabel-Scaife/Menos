
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCasting : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickable;

    public void OnFire(InputAction.CallbackContext context)
    {
        // on click run
        if (context.phase == InputActionPhase.Started)
        {
            ToolClicks();
        }
    }
  

    /// <summary>
    /// Run when there are clicks on scenes with tools 
    /// </summary>
    private void ToolClicks()
    {
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit;   
        Tool tool = ToolManager.Instance.CurrentTool;

        // check if anything was hit 
        hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, clickable).collider;

        if (hit != null || (hit == null && tool != null))
        {
            if(HoldingTool(hit, tool))
            {
                return;
            }

            if (PickUpTool(hit))
            {
                return;
            }
        }
    }


    /// <summary>
    /// if holding a tool perform action
    /// </summary>
    /// <returns>true performed tool actoin, false not holding tool</returns>
    private bool HoldingTool(Collider2D objectHit, Tool tool)
    {   
        if (tool != null)
        {
            if (objectHit == null)
            {
                tool.DropTool();
            }
            else
            {
                tool.Use();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Pick up tool 
    /// </summary>
    /// <returns>ture if hit, false if not</returns>
    private bool PickUpTool(Collider2D objectHit)
    {
        if (objectHit.CompareTag("Tool"))
        {
            objectHit.gameObject.GetComponent<Tool>().SelectTool();
            return true;
        }
        return false;
    }
}
