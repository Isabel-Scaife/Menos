using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : PlayerControlled
{
    [SerializeField]
    private GameObject heldObject = null;

    public void Drop()
    {
        // remove item from bird
        heldObject.transform.SetParent(null);
        heldObject = null;

    }

    public override void Interact(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // only interact if not holding item and in range
            if (heldObject == null && interactObject != null)
            {
                base.Interact(context);
                //heldObject = interactObject.gameObject;
            }
            // drop item that is held 
            else if (heldObject != null)
            {
                Drop();
            }
        }

    }

    /// <summary>
    /// If holding no item, pick up item
    /// </summary>
    /// <param name="item">object being picked up</param>
    /// <returns>
    /// true if item picked up, 
    /// false if not 
    /// </returns>
    public bool Pickup(GameObject item)
    {
        if(heldObject == null)
        {
            heldObject = item;
            return true;
        }
        return false;
    }


}
