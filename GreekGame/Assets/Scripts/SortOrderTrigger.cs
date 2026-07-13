using UnityEngine;

public class SortOrderTrigger : MonoBehaviour
{
    [SerializeField] private int sortOrder;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            collision.GetComponent<Bird>().ChangeSortOrder(sortOrder);
        }
    }
}
