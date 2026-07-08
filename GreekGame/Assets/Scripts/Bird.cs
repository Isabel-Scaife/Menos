using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : PlayerControlled
{
    [SerializeField] private GameObject heldObject = null;

    private Vector2 acceleration, steeringForce;

    [Header("Non-Player control movement")]
    [Header("Seek")]
    [SerializeField, Range(0, 1f)] private float seekWeight;
    [SerializeField] private GameObject seekTarget;

    [Header("Evade")]
    [SerializeField, Range(0, 1f)] private float evadeWeight;
    [SerializeField] private LayerMask evadeTarget;
    [SerializeField] private float evadeRadius;

    [Header("Wander")]
    [SerializeField, Range(0, 1f)] private float wanderWeight; 
    protected private Vector2 wanderTarget;

    private Vector2 totalForce;

    [SerializeField] private float catchUpRadius;
    [SerializeField] private float matchSpeedRadius;

    [SerializeField] private float matchSpeed;

    private Vector2 seekForce, evadeForce;

    public override void Move(InputAction.CallbackContext context)
    {
        if(controlBird) base.Move(context);
    }

    public override void Interact(InputAction.CallbackContext context)
    {
        // no interaction if not controlling bird or bird is already holding an item
        if (!controlBird || heldObject != null) return;

        base.Interact(context);
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
        if(!controlBird)
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
        else base.FixedUpdate();
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
            seekForce += Seek(targetPos, speed) * seekWeight;
            evadeForce += Evade(.6f, speed) * evadeWeight;
        }
        else if(distance >= matchSpeedRadius)
        {
            seekForce += Seek(targetPos, matchSpeed) * seekWeight;
            evadeForce += Evade(.6f, matchSpeed) * evadeWeight;
        }
        totalForce += Wander(.5f, 1.5f, 2f) * wanderWeight;

        totalForce += seekForce + evadeForce;

        return totalForce;
    }

    private Vector2 Evade(float timeInSeconds, float currentSpeed)
    {
        // evade objects that are detected in range of futuer position 
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(
            GetFuturePosition(timeInSeconds),
            evadeRadius,
            velocity.normalized,
            10,
            evadeTarget);

        if (hit.collider != null)
        {
            Vector2 desiredVelocity = transform.position - hit.transform.position;
            desiredVelocity = desiredVelocity.normalized * currentSpeed;

            return desiredVelocity - velocity;
        }

        return Vector2.zero;
    
    }

    /// <summary>
    /// Move in a tandom direction with a circle in front 
    /// </summary>
    /// <param name="wanderRadius">radius of circle</param>
    /// <param name="wanderDistance">how far center circle in front</param>
    /// <returns></returns>
    protected Vector2 Wander(float wanderRadius, float wanderDistance, float wanderJitter)
    {
        if (wanderTarget == Vector2.zero)
        {
            wanderTarget = Random.insideUnitSphere.normalized * wanderRadius;
        }

        wanderJitter *= Time.deltaTime;

        wanderTarget += new Vector2(
            Random.Range(-1f, 1f) * wanderJitter,
            Random.Range(-1f, 1f) * wanderJitter
            );

        wanderTarget = wanderTarget.normalized * wanderRadius;

        Vector2 targetInWorldSpace = (Vector2)transform.position +
            (velocity.normalized * wanderDistance) + wanderTarget;

        return Seek(targetInWorldSpace, speed);
    }


    protected Vector2 GetFuturePosition(float timeInSeconds)
    {
        Vector2 futurePos = (Vector2)transform.position + velocity * timeInSeconds;

        return futurePos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, seekForce);

        Gizmos.DrawWireSphere(GetFuturePosition(.5f), 1.5f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, evadeForce);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, totalForce);

        Gizmos.DrawWireSphere(GetFuturePosition(.6f), 0.25f);

    }
}
