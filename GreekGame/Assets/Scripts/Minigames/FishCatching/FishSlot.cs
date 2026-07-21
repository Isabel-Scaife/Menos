using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class FishSlot : MonoBehaviour
{
    private FishToSort fishHovering;
    private FishToSort fishInPlace;
    private bool correct = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int id;
    public Hand hand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //snapping into place mechanic
        if (Input.GetMouseButtonDown(1) && fishHovering != null)
        {
            //drops it and snaps in place
            hand.fishInHand = null;
            fishHovering.gameObject.transform.position = this.transform.position;

            //sets current fish
            fishInPlace = fishHovering;
        }

        //if there is a fish snapped but picked up
        if (fishInPlace != null && hand.fishInHand != null)
        {
            fishInPlace = null;
        }

        //if there is a fish snapped
        if (fishInPlace != null)
        {
            hand.fishInHand = null;
            fishInPlace.gameObject.transform.position = this.transform.position;
        }

        //if there is a fish snapped + to check if correct
        if (fishInPlace != null && fishInPlace.id == this.id)
        {
            this.spriteRenderer.color = Color.antiqueWhite;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sets it to fish that is hoverirng
        fishHovering = collision.gameObject.GetComponent<FishToSort>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        fishHovering = null;
    }
}
