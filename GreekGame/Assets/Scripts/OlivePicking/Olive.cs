using UnityEngine;

public class Olive : MonoBehaviour
{
    [SerializeField]
    private OlivesBird bird;
    private Collider2D birdCollider;
    private Collider2D oliveCollider;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets necessary colliders and such
        rb = GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        oliveCollider = GetComponent<Collider2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "OlivesBird")
        {
            //turns gravity on
            rb.gravityScale = 1;
            //turns trigger off so it collides with gound
            oliveCollider.isTrigger = false;
        }
    }
}
