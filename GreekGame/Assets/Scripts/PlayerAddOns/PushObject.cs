using UnityEngine;
using UnityEngine.InputSystem;

public class ApplyForce : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float forceMagnitude;

    [SerializeField]
    private LayerMask pushableObjectLayer;

    // would be better to have an entity parent (or interface) with a direction 
    // that has playercontoredll, guards, npcs as children 
    // so we could have non playable characters push objects

    // need direction of the object moving the pushable object 
    [SerializeField]
    PlayerControlled playerControlled;

    private void Awake()
    {
        playerControlled = GetComponent<PlayerControlled>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if ((pushableObjectLayer & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log("collided with a force applier");

            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

            // determine is player is walking in same direction as applied force
            Vector2 normal = collision.contacts[0].normal;

            float dot = Vector2.Dot(playerControlled.Direction, -normal);

            if (dot > 0.5f)
            {
                rb.AddForce(forceMagnitude * playerControlled.Direction * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }

    }
}
