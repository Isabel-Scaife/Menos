using UnityEngine;

public class Key : Item
{
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
                this.transform.SetLocalPositionAndRotation(new Vector2 (0, 4.4f), this.transform.localRotation);
                held = true;
            }
        }
        // destory key if it's not currently held
        else if (player is Player && held)
        {
            // set a flag to mark this has been obtained
            if (GameStateManager.Instance == null) Debug.Log("No GameStateManager in scene");
            else GameStateManager.Instance.SetFlag(collectedFlag);

            base.Interact(player);
        }
    }
}
