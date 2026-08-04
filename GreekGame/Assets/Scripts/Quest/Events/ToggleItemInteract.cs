using UnityEngine;

public class ToggleItemInteract : MonoBehaviour, IEvent
{
    [SerializeField] private Item item;
    [SerializeField] private bool activate;

    public void OnQuestComplete()
    {
        if (item != null)
        {
            item.CanInteract = activate;
        }
    }
}
