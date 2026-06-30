using UnityEngine;

public class Key : Item
{
    [SerializeField]
    private Door door;

    [SerializeField]
    public EvidenceData key;

    [SerializeField]
    private string collectedFlag;

    public override void Interact(PlayerControlled player)
    {
        // place object in bird inventory
        if (player is Bird)
        {
            Bird bird = (Bird)player;

            if (bird.Pickup(this.gameObject))
            {
                this.transform.SetParent(bird.transform);
            }
        }
        // destory key if it's not currently held
        else if (player is Player && transform.parent == null)
        {
            key.discovered = true;

            Debug.Log("Key Discovered: " + key.discovered);

            // remove ID from spawn manager
            if (SpawnManager.Instance == null)
            {
                Debug.Log("No SpawnManager in scene");
            }
            else
            {
                SpawnManager.Instance.RemoveItem(itemID);
            }

            // set a flag to mark this has been obtained
            if (GameStateManager.Instance == null) Debug.Log("No GameStateManager in scene");
            else GameStateManager.Instance.SetFlag(collectedFlag);

            base.Interact(player);
        }
    }
}
