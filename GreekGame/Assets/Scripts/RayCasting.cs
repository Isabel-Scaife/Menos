using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RayCasting : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickable;
    [SerializeField]
    private LayerMask clickableNoTool;

    public void OnFire(InputAction.CallbackContext context)
    {

        string currentScene = SceneManager.GetActiveScene().name;

        // track mouse phases if holding is necessary
        if (context.phase == InputActionPhase.Started)
        {
            
            // run package raycasting checks
            //if(currentScene == "Package")
            //{
            //    PackageSceneClick();
            //}
            if (currentScene == "PotPackage")
            {
                PotPackageClick();
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {

            //// run package raycasting checks
            //if (currentScene == "Package")
            //{
            //    PackageSceneReset();
            //}
        }
    }
  

    /// <summary>
    /// Raycasting checks in Pot scene
    /// </summary>
    private void PotPackageClick()
    {
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit;   
        Tool tool = PackageManager.Instance.CurrentTool;

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

    /// <summary>
    /// Checks if letter was clicked,
    /// scene rec: layer 7, z (1 ,10) ), proper layer
    /// </summary>
    /// <param name="worldPos">mouse position</param>
    /// <returns>true if hit, false if not </returns>
    private bool PullLetter(Vector3 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, clickable, 1f);

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

    ///// <summary>
    ///// Raycasting checks in Package scene
    ///// </summary>
    //private void PackageSceneClick()
    //{
    //    Tool tool = PackageManager.Instance.CurrentTool;
    //    Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

    //    // holding tool action
    //    if(HoldingTool(tool, worldPos))
    //    {
    //        return;
    //    }

    //    // holding nothing
    //    if(tool == null)
    //    {
    //        // pick up tool 
    //        if (PickUpTool(worldPos))
    //        {
    //            return;
    //        }
    //            // pull letter out
    //        if (PullLetter(worldPos))
    //        {
    //            return;
    //        }

    //        // otherwise start tracking drag for envelope 
    //        PackageManager.Instance.MailObj.GetComponent<Mail>().Raycast();
    //    }
    //}
    ///// <summary>
    ///// Reset click in package scene 
    ///// </summary>
    //private void PackageSceneReset()
    //{
    //    Tool tool = PackageManager.Instance.CurrentTool;

    //    // holding tool, reset tool  
    //    if (tool != null)
    //    {
    //        tool.ResetUse();
    //    }

    //    // holding nothing, reset drag for mail  
    //    if (tool == null)
    //    {
    //        PackageManager.Instance.MailObj.GetComponent<Mail>().Dragging = false;
    //        PackageManager.Instance.Letter.Dragging = false;
    //    }

}
