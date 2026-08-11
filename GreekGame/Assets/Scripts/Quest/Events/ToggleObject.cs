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
