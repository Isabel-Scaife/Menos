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

    /// <summary>
    /// switches to scrollable letter
    /// </summary>
    public void OpenLetter()
    {
        initialLetterUI.SetActive(false);
        scrollingLetterUI.SetActive(true);
    }

    /// <summary>
    /// goes back to minigame without showing scrollable letter UI
    /// </summary>
    public void SkipReading()
    {
        initialLetterUI.SetActive(false);
        spriteRenderer.enabled = true;
    }

    /// <summary>
    /// hides letter UI and goes back to minigame
    /// </summary>
    public void CloseLetter()
    {
        scrollingLetterUI.SetActive(false);
        spriteRenderer.enabled = true;
    }
}
