using UnityEngine;
using UnityEngine.InputSystem;

public class FishToSort : MonoBehaviour
{
    public bool grabbed;
    private Vector2 mousePosition;
    public SpriteRenderer spriteRenderer;

    //fish parameters
    public float size;
    public Color color;
    public int id;

    public FishToSort(float size, Color color)
    {
        this.size = size;
        this.color = color;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();

        //sets fish physical appearance
        transform.localScale = new Vector3(size/2, size/2, 1);
        //spriteRenderer.color = color;
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
    }

    //detetcs when moused over and clicked on
    //for fih pickup
    private void OnMouseDown()
    {
        grabbed = !grabbed;
    }

}
