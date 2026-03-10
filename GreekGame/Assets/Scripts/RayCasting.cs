using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RayCasting : MonoBehaviour
{
    [SerializeField]
    private List<LayerMask> interactableLayers;

    // these are only used for checking distances,
    // only hooked up for personal testing
    [SerializeField]
    private float distance;

    [SerializeField]
    private float minDist;

    public void OnFire(InputAction.CallbackContext context)
    {

        string currentScene = SceneManager.GetActiveScene().name;

        // track mouse phases if holding is necessary
        if (context.phase == InputActionPhase.Started)
        {
            
            // run package raycasting checks
            if(currentScene == "Package")
            {
                PackageSceneClick();
            }
            else if (currentScene == "PotPackage")
            {
                PotPackageClick();
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {

            // run package raycasting checks
            if (currentScene == "Package")
            {
                PackageSceneReset();
            }
        }
    }

   
    /// <summary>
    /// Raycasting checks in Package scene
    /// </summary>
    private void PackageSceneClick()
    {
        Tool tool = PackageManager.Instance.CurrentTool;
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // holding tool action
        if(HeldToolChecks(tool, worldPos))
        {
            return;
        }

        // holding nothing
        if(tool == null)
        {
            // pick up tool 
            if (PickUpTool(worldPos))
            {
                return;
            }
                // pull letter out
            if (PullLetter(worldPos))
            {
                return;
            }

            // otherwise start tracking drag for envelope 
            PackageManager.Instance.MailObj.GetComponent<Mail>().Raycast();

            // evelope hit 
            // ====================== i think this is obsolete and can be removed I dont remember though ================
            //hit = Physics2D.Raycast(worldPos, Vector2.zero, distance, interactableLayers[1], minDist);

            //if (hit.collider != null)
            //{
            //    Debug.Log(hit.collider.gameObject.name);
            //    PackageManager.Instance.MailObj.GetComponent<Mail>().Raycast();
            //    return;
            //}
        }
    }
    /// <summary>
    /// Reset click in package scene 
    /// </summary>
    private void PackageSceneReset()
    {
        Tool tool = PackageManager.Instance.CurrentTool;

        // holding tool, reset tool  
        if (tool != null)
        {
            tool.ResetUse();
        }

        // holding nothing, reset drag for mail  
        if (tool == null)
        {
            PackageManager.Instance.MailObj.GetComponent<Mail>().Dragging = false;
            PackageManager.Instance.Letter.Dragging = false;
        }
    }

    /// <summary>
    /// Raycasting checks in Pot scene
    /// </summary>
    private void PotPackageClick()
    {
        Tool tool = PackageManager.Instance.CurrentTool;
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // holding tool action
        if (HeldToolChecks(tool, worldPos))
        {
            return;
        }

        // pickup up tool if clicked 
        if (tool == null)
        {
            if(PickUpTool(worldPos))
            {
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, -1, 0f);
            
            if(hit.collider != null && hit.collider.CompareTag("Button"))
            {
                // current does not work like an actual button 
                VasePackage.Instance.ResetImage();
            }
            else if (hit.collider == null)
            {
                Debug.Log("Nothing hit");
            }
        }
    }


    /// <summary>
    /// drop current tool or start complete tool click action 
    /// scene rec: z (1 ,10) )
    /// </summary>
    /// <param name="tool">tool held</param>
    /// <param name="worldPos">mouase position</param>
    /// <returns>true if have holding item, false if not</returns>
    private bool HeldToolChecks(Tool tool, Vector3 worldPos)
    {
        if (tool != null)
        {
            RaycastHit2D hit;

            // drop tool 
            hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, -1, 1f);

            if (hit.collider == null)
            {
                tool.DropTool();
            }
            // use tool 
            else
            {
                tool.RayCast();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Pick up tool 
    /// scene rec: z (1 ,10) ), proper layer
    /// </summary>
    /// <param name="worldPos">mouse position</param>
    /// <returns>ture if hit, false if not</returns>
    private bool PickUpTool(Vector3 worldPos)
    {
        // pick up tool 
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, interactableLayers[0], 1f);

        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Tool>().SelectTool();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if letter was clicked,
    /// scene rec: layer 7, z (1 ,10) ), proper layer
    /// </summary>
    /// <param name="worldPos">mouse position</param>
    /// <returns>true if hit, false if not </returns>
    private bool PullLetter(Vector3 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, interactableLayers[2], 1f);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 7)
            {
                hit.collider.gameObject.GetComponent<Letter>().Raycast();
                return true;
            }
        }
        return false;
    }

}
