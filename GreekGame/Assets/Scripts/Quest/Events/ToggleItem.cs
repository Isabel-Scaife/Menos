using UnityEngine;

public class ToggleItem : IEvent
{
    [SerializeField] private GameObject item;
    [SerializeField] private bool activate; // be inactive/active

    public void OnQuestComplete()
    {
        if(item != null)
        {
            item.SetActive(activate);
        }
    }

}
