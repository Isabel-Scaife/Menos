using UnityEngine;

[System.Serializable]
public class ToggleItemInteract : QuestEvent
{
    // need a way to get the item
    [SerializeField] private Item item;
    [SerializeField] private bool activate;

    public override void PlayEvent()
    {
        if (item != null)
        {
            item.CanInteract = activate;
        }
    }
}
