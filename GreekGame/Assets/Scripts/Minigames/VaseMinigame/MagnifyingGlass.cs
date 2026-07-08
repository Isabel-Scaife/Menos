using UnityEngine;
using UnityEngine.InputSystem;

public interface IRaycast
{
    public void Interact();
}


public class MagnifyingGlass : Tool
{

    [SerializeField]
    SpriteRenderer detectZone;

    public bool canDrop = false;

    /// <summary>
    /// Interact with object clicked
    /// May add information to journal or cause something to move
    /// </summary>
    public override void Use() 
    {
        Collider2D hit;
        hit = Physics2D.Raycast(mouse.MousePostion, Vector2.zero, 10.0f, clickable).collider;

        if (hit != null)
        {
            IRaycast raycast = hit.GetComponent<IRaycast>();
            if (raycast != null)
            {
                raycast.Interact();
            }
        }
    }

    public override void DropTool()
    {   
        if(canDrop) base.DropTool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectZone.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectZone.enabled = false;
    }
}
