
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCasting : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickable;

    /// <summary>
    /// Do a raycast when mouse is clicked
    /// </summary>
    public void OnFire(InputAction.CallbackContext context)
    {
        // on click run
        if (context.phase != InputActionPhase.Started) return;

        // do a raycast
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit;
        hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, clickable).collider;

        // run methods on hit collider
        ToolClicks(hit);

        // return if nothing was hit
        if (hit == null) return;
        DiceClicks(hit);
    }


    /// <summary>
    /// Run when there are clicks on scenes with tools 
    /// </summary>
    private void ToolClicks(Collider2D hit)
    {
        Tool tool = ToolManager.Instance.CurrentTool;

        if (hit != null || (hit == null && tool != null))
        {
            if(HoldingTool(hit, tool)) return;

            if (PickUpTool(hit)) return;
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
    /// <returns>true if hit, false if not</returns>
    private bool PickUpTool(Collider2D objectHit)
    {
        if (objectHit.CompareTag("Tool"))
        {
            objectHit.gameObject.GetComponent<Tool>().SelectTool();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Calls methods on a dice that's clicked
    /// </summary>
    /// <param name="hit">object clicked</param>
    private void DiceClicks(Collider2D hit)
    {
        Dice script = hit.GetComponent<Dice>();
        if (script == null) return;
        script.SelectOrDeselect();
    }
}
