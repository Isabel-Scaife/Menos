using UnityEngine;

/// <summary>
/// Item that, when interacted with, shows a popup on a UI canvas
/// </summary>
public class PopupItem : Interactable
{
    // fields
    [SerializeField]
    private Sprite popupSprite;

    // shows popup on canvas when interacted with
    public override void Interact(PlayerControlled player)
    {
        // maybe adds itself to player's inventory
        PopupManager.Instance.ShowPopup(popupSprite);
    }
}
