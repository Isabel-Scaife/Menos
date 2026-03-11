using UnityEngine;
using UnityEngine.InputSystem;

public class Letter : MonoBehaviour
{
    // fields
    [SerializeField]
    private float letterDragDist = 80f;
    [SerializeField]
    protected float currentDragDist = 0;
    [SerializeField]
    private bool dragging = false;

    // UI related fields
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject initialLetterUI;
    [SerializeField]
    private GameObject scrollingLetterUI;

    public bool Dragging
    {
        get => dragging;
        set
        {
            dragging = value;
            currentDragDist = 0;
        }
    }


    void Update()
    {
        if (dragging)
        {
            Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit;
            hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, -1, 7f, 7f);

            // letter hit, drag out letter  
            if (hit.collider != null)
            {
                // debug message
                Debug.Log(hit.collider.gameObject.name);

                // update change in mouse y  
                currentDragDist += Mouse.current.delta.ReadValue().y;

                // drag enough to pull out 
                if (currentDragDist >= letterDragDist)
                {
                    // show canvas and hide letter sprite
                    // maybe replace this with an animation later
                    spriteRenderer.enabled = false;
                    initialLetterUI.SetActive(true);

                    // visuals
                    //      letter in front with read/or not option
                    //
                    //      read unflods letters for user
                    //      scroll through letter
                    //      closes when user clicks close option       
                    //
                    //      skip noting happens and letter minigame continues

                    // reset drag
                    currentDragDist = 0;
                }
            }
        }
    }

    public void Raycast()
    {
        dragging = true;
    }

    // what happens when player chooses to read letter
    public void OnOpen()
    {

    }

    // what happens when player chooses not to read letter
    public void OnDecline()
    {

    }

    // what happens when player finishes reading
    public void OnClose()
    {
        scrollingLetterUI.SetActive(false);
        spriteRenderer.enabled = true;
    }
}
