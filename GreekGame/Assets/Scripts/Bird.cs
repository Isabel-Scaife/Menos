using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : PlayerControlled
{
    [SerializeField]
    private GameObject heldObject = null;

    private Vector2 acceleration, steeringForce;

    [SerializeField]
    private float seekWeight;
    [SerializeField]
    private GameObject seekTarget;
    private Vector2 seekForce, totalForce;

    [SerializeField]
    private float catchUpRadius;
    [SerializeField] 
    private float matchSpeedRadius;

    [SerializeField]
    private float matchSpeed;

    public void Drop()
    {
        // remove item from bird
        heldObject.transform.SetParent(null);
        heldObject = null;

    }

    public override void Interact(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // only interact if not holding item and in range
            if (heldObject == null && interactObject != null)
            {
                base.Interact(context);
                //heldObject = interactObject.gameObject;
            }
            // drop item that is held 
            else if (heldObject != null)
            {
                Drop();
            }
        }

    }

    /// <summary>
    /// If holding no item, pick up item
    /// </summary>
    /// <param name="item">object being picked up</param>
    /// <returns>
    /// true if item picked up, 
    /// false if not 
    /// </returns>
    public bool Pickup(GameObject item)
    {
        if(heldObject == null)
        {
            heldObject = item;
            return true;
        }
        return false;
    }

    protected override void FixedUpdate()
    {
        // bird automoved if not controlled
        if(controlBird)
        {
            // determine acceleration
            acceleration = Vector2.zero;
            steeringForce = CalcSteering();
            acceleration += steeringForce;

            // update velocity 
            velocity += acceleration * Time.fixedDeltaTime;
            velocity = Vector2.ClampMagnitude(velocity, speed);

            // update position
            Vector2 nextPos = (Vector2) transform.position + velocity * Time.fixedDeltaTime;
            rb.MovePosition(nextPos);
        }
        else
        {
            base.FixedUpdate();
        }
    }

    // methods that help determine bird movement when not controlled


    public Vector2 Seek(Vector2 targetPos, float currentSpeed)
    {
        Vector2 desiredVelocity = targetPos - (Vector2)transform.position;
        desiredVelocity = desiredVelocity.normalized * currentSpeed;

        return desiredVelocity - velocity;
    }

    private Vector2 CalcSteering()
    {
        totalForce = Vector2.zero;

        // check distance from target 
        Vector2 targetPos = seekTarget.transform.position;
        float distance = Vector2.Distance(targetPos, transform.position);

        if(distance >= catchUpRadius)
        {
            totalForce += Seek(targetPos, speed);
        }
        else if(distance >= matchSpeed)
        {
            totalForce += Seek(targetPos, matchSpeed);
        }

        return totalForce;
    }
}
