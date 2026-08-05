using UnityEngine;

public class ToggleInteractable : MonoBehaviour, IEvent
{
    [SerializeField] private Interactable interactable;
    [SerializeField] private bool activate;

    public void OnQuestComplete()
    {
        if (interactable != null)
        {
            interactable.enabled = activate;
        }
    }
}
