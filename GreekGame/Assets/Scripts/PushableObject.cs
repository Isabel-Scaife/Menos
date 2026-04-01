using Unity.VisualScripting;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    Rigidbody2D rb; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControlled playerControlled = collision.gameObject.GetComponent<PlayerControlled>();
        if (playerControlled != null)
        {
            if (playerControlled is Bird)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                rb.bodyType= RigidbodyType2D.Dynamic;
            }
        }
    }
}
