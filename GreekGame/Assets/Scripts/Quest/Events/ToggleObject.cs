using UnityEngine;

[System.Serializable]
public class ToggleObject : MonoBehaviour, IEvent
{
    [SerializeField] private GameObject item;
    [SerializeField] private bool activate; // be inactive/active

    public void OnQuestComplete()
    {
        Debug.Log("item toggled");
        if (item != null)
        {
            item.SetActive(activate);
        }
    }

}
