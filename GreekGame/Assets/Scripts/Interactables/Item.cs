using System;
using UnityEngine;

public class Item : Interactable
{
    // might change how this works from 
    [SerializeField] protected string itemID;
    public event Action OnCollect;

    public string ItemID { get => itemID; private set => itemID=value; }

    public override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

        // TODO: add item to inventory

        // methods that run when item is picked up 
        OnCollect.Invoke();
        
        Destroy(this.gameObject);
    }

}
