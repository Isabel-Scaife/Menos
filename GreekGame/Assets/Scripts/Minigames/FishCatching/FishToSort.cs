using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.StickyNote;

public class FishToSort : MonoBehaviour
{
    public bool grabbed;
    private Vector2 mousePosition;
    public SpriteRenderer spriteRenderer;

    //fish parameters
    public float size;
    public float colorNumR;
    public float colorNumG;
    public float colorNumB;
    public int id;

    public FishToSort(float size, float colorNumR, float colorNumG, float colorNumB)
    {
        this.size = size;
        this.colorNumR = colorNumR;
        this.colorNumG = colorNumG;
        this.colorNumB = colorNumB;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();

        //sets fish physical appearance
        transform.localScale = new Vector3(size/2, size/2, 1);
        spriteRenderer.color = new UnityEngine.Color(colorNumR, colorNumG, colorNumB, 1);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //Pickks up fih
        if (grabbed)
        {
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 1);
        }

        //drops fish
        if (grabbed && Input.GetMouseButtonDown(1))
        {
            grabbed = false;
        }

    }

    //detetcs when moused over and clicked on
    //for fih pickup
    private void OnMouseDown()
    {
        grabbed = true;
    }

}
