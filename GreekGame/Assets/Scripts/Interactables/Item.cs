using System;
using UnityEngine;

public class Item : Interactable
{
    // might change how this works from 
    [Header("Item Info")]
    [SerializeField] protected string itemID;
    [SerializeField] protected bool playerCanInteract = true;
    [SerializeField] private string collectedFlag;

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
        // place object in bird inventory
        if (player is Bird)
        {
            Bird bird = (Bird)player;

            if (bird.Pickup(this.gameObject))
            {
                this.transform.SetParent(bird.transform);
                this.transform.SetLocalPositionAndRotation(new Vector2(0, 4.4f), this.transform.localRotation);
                held = true;
            }
        }
        // destroy item if it's not currently held
        else if (player is Player && held)
        {
            // set a flag to mark this has been obtained
            if (GameStateManager.Instance == null) Debug.Log("No GameStateManager in scene");
            else GameStateManager.Instance.SetFlag(collectedFlag);

            // methods that run when item is picked up 
            if (OnCollect != null) OnCollect.Invoke();

            if (SpawnManager.Instance == null) { Debug.Log("Missing Spawn Manger"); return; }

            SpawnManager.Instance.RemoveItem(itemID);
            Destroy(this.gameObject);
        }
        else if (player is Player)
        {
            if (OnCollect != null) OnCollect.Invoke();

            if (SpawnManager.Instance == null) { Debug.Log("Missing Spawn Manger"); return; }

            SpawnManager.Instance.RemoveItem(itemID);
            Destroy(this.gameObject);
        }
    }

}
