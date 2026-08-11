using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "ToggleItemInteract", menuName = "Events/Toggle/ItemInteract")]
public class ToggleItemInteract : QuestEvent
{
    [SerializeField] private bool activate;

    public override void PlayEvent()
    {
        // get item script on object 
        Item item = gameObject.GetComponent<Item>();

        if (item != null)
        {
            item.CanInteract = activate;
        }
    }
}
