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
        // methods that run when item is picked up 
        if(OnCollect != null) OnCollect.Invoke();

        if(SpawnManager.Instance == null) { Debug.Log("Missing Spawn Manger"); return; }
        
        SpawnManager.Instance.RemoveItem(itemID);
        Destroy(this.gameObject);
    }

}
