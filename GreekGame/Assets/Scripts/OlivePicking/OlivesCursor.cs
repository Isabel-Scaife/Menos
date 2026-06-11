using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class OlivesCursor : MonoBehaviour
{

    // fields
    private SpriteRenderer spriteRenderer;
    private Vector3 birdTarget;

    [SerializeField]
    private OlivesBird bird;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y,0);

        //moves bird towards target
        //called evey frame
        bird.MoveTo(birdTarget);

        //if mouse is on upper half of screen-- control bird
        if (mousePosition.y >= 0)
        {
            spriteRenderer.color = Color.blue;
            //sets bird target wherever player clicks
            if (Input.GetMouseButtonDown(0))
            {
                birdTarget = transform.position;
            }
        }
        else // mouse is on lower half-- control hand
        {
            spriteRenderer.color = Color.red;
        }
    }
}
