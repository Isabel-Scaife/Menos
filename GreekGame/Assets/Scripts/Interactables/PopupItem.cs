using UnityEngine;

/// <summary>
/// Item that, when interacted with, shows a popup on a UI canvas
/// </summary>
public class PopupItem : Interactable
{
    // fields
    [SerializeField]
    private Sprite popupSprite;

    /// <summary>
    /// shows popup on canvas when interacted with
    /// </summary>
    /// <param name="player">player who interacted with this</param>
    public override void Interact(PlayerControlled player)
    {
        // maybe adds itself to player's inventory
        // logs error if there is no instance, otherwise shows popup
        if (PopupManager.Instance == null)
        {
            Debug.Log("PopupManager instance does not exist");
            return;
        }
        PopupManager.Instance.ShowPopup(popupSprite, player);
    }
}
