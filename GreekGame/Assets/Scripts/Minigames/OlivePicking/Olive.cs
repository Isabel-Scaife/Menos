using UnityEngine;

public class Olive : Tool
{
    private Collider2D objCollider;
    private Rigidbody2D rb;

    private Vector3 parentPos;

    void Start()
    {
        //gets necessary colliders and such
        rb = GetComponent<Rigidbody2D>();
        objCollider = GetComponent<Collider2D>();
        
        parentPos = transform.parent.position;

        rb.gravityScale = 0;
    }

    public override void SelectTool()
    {
        // only pick if on bottom of screen 
        if (mouse.MousePostion.y - parentPos.y < 1) base.SelectTool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            // drop olive on ground 
            rb.gravityScale = 1;
            objCollider.isTrigger = false;
        }
    }
}
