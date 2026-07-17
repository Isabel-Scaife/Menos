using UnityEngine;
using UnityEngine.Rendering;

public class SortOrderTrigger : MonoBehaviour
{
    [SerializeField] private int sortOrder;
    [SerializeField] private bool playerInteractOn;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name + ", " + collision.tag);
        if (collision.CompareTag("Bird"))
        {
            Debug.Log("run");
            Bird bird = collision.GetComponentInParent<Bird>();
            bird.ChangeSortOrder(sortOrder);

            Item held = bird.GetItemHeld();
            if (held != null)
            {
                //held.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
                held.CanInteract = playerInteractOn;
            }
        }
    }
}
