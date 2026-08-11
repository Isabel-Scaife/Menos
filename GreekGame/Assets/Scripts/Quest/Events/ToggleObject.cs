using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "Toggle", menuName = "Events/Toggle")]
public class ToggleObject : QuestEvent
{
    [SerializeField] private GameObject item;
    [SerializeField] private bool activate; // be inactive/active

    public override void PlayEvent()
    {
        if (item != null)
        {
            item.SetActive(activate);
        }
    }

    // create new script
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (item != null)
    //    {
    //        item.SetActive(activate);
    //    }
    //}

}
