using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "ToggleObject", menuName = "Events/Toggle/Object")]
public class ToggleObject : QuestEvent
{
    [SerializeField] private bool activate; // be inactive/active

    public override void PlayEvent()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(activate);
        }
        else
        {
            Debug.Log("object null");
        }
    }

}
