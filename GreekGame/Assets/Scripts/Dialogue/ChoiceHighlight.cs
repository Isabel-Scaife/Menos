using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Shows an image on the currently selected choice box as an indicator
/// </summary>
public class ChoiceHighlight : MonoBehaviour,
    ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private GameObject highlight;

    private void OnEnable()
    {
        highlight.SetActive(false);
    }

    private void OnDisable()
    {
        highlight.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        highlight.SetActive(false);
    }
}
