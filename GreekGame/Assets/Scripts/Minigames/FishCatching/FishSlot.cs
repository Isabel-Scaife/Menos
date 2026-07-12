using UnityEngine;

public class FishSlot : MonoBehaviour
{
    private FishToSort fishHovering;
    private FishToSort fishInPlace;
    private bool correct = false;

    [SerializeField]
    private int id;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sets it to fish that is hoverirng
        fishHovering = collision.gameObject.GetComponent<FishToSort>();
        Debug.Log("Fish hoveirnrgh");

        //if it is not being grabbed by player, it is no longer hovering
        if (fishHovering.grabbed == false)
        {
            fishHovering = null;
            Debug.Log("Fish touching slab, but not held by player");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        fishHovering = null;
        Debug.Log("No longerr hovering");
    }

    private void OnMouseDown()
    {
        //snapping into place mechanic
        if (fishHovering != null)
        {
            //drops it and snaps in place
            //fishHovering.grabbed = false;
            fishHovering.gameObject.transform.position = this.transform.position;

            //sets current fish
            fishInPlace = fishHovering;
            Debug.Log("Snapped");
        }
    }
}
