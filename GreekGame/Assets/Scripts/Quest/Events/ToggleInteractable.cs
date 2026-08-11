using UnityEngine;

[System.Serializable]
public class ToggleInteractable : QuestEvent
{
    [SerializeField] private Interactable interactable;
    [SerializeField] private bool activate;

    public override void PlayEvent()
    {
        if (interactable != null)
        {
            interactable.enabled = activate;
        }
    }
}
