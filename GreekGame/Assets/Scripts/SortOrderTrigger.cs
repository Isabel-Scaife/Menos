using UnityEngine;

public class SortOrderTrigger : MonoBehaviour
{
    [SerializeField] private int sortOrder;
    [SerializeField] private bool playerInteractOn;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            Bird bird = collision.GetComponent<Bird>();
            bird.ChangeSortOrder(sortOrder);

            Item held = bird.GetItemHeld();
            if (held != null)
            {
                held.CanInteract = playerInteractOn;
            }
        }
    }
}
