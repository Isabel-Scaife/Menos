using System;
using UnityEngine;

public class Item : Interactable
{
    // might change how this works from 
    [Header("Item Info")]
    [SerializeField] protected string itemID;
    [SerializeField] protected bool playerCanInteract = true;

    public event Action OnCollect;

    protected bool held = false;

    public bool CanInteract { get => playerCanInteract; set => playerCanInteract = value; }

    /// <summary>
    /// whether or not this item is currently the bird's held item
    /// </summary>
    public bool HeldByBird
    {
        get { return held; }
    }

    public string ItemID { get => itemID; private set => itemID=value; }

    public override void Interact(PlayerControlled player)
    {
        // methods that run when item is picked up 
        if(OnCollect != null) OnCollect.Invoke();

        if(SpawnManager.Instance == null) { Debug.Log("Missing Spawn Manger"); return; }
        
        SpawnManager.Instance.RemoveItem(itemID);
        Destroy(this.gameObject);
    }

}
