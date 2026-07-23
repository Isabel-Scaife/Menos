
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Bird : PlayerControlled
{
    [SerializeField] private SortingGroup sortGroup;

    [SerializeField] private GameObject heldObject = null;

    private Vector2 acceleration, steeringForce;

    [Header("Non-Player control movement")]
    private bool follow = true;
    private float offset = -6.5f;

    [Header("Arrive")]
    [SerializeField, Range(0, 1f)] private float arriveWeight;
    [SerializeField] private GameObject arriveTarget;
    [SerializeField] private float slowRad;

    [Header("Evade")]
    [SerializeField, Range(0, 1f)] private float evadeWeight;
    [SerializeField] private LayerMask evadeTarget;
    [SerializeField] private float evadeRadius;

    [Header("Wander")]
    [SerializeField, Range(0, 1f)] private float wanderWeight;
    [SerializeField] private float wanderDuration;
    [SerializeField] private float wanderRad;
    [SerializeField] private float wanderDis;
    [SerializeField] private float wanderJ;
    private Vector2 wanderTarget;
    private float wanderTimer;

    [Header("Distance from Player")]
    [SerializeField] private float seekDistance;

    private Vector2 totalForce, arriveForce, evadeForce, wanderForce;

    protected override void Awake()
    {
        wanderTimer = wanderDuration;
        base.Awake();
    }

    public override void Move(InputAction.CallbackContext context)
    {
        if (controlBird) base.Move(context);
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
        if(controlBird) base.FixedUpdate();

        if (!follow) return;

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
    }

    // methods that help determine bird movement when not controlled

    public Vector2 Seek(Vector2 targetPos)
    {
        Vector2 desiredVelocity = targetPos - (Vector2)transform.position;
        desiredVelocity = desiredVelocity.normalized * speed;

        return desiredVelocity - velocity;
    }

    private Vector2 Arrive(Vector2 targetPos, float slowRadius)
    {
        Vector2 distance = targetPos - (Vector2)transform.position;
        float arriveSpeed = speed * (distance.magnitude / slowRadius);
        arriveSpeed = Mathf.Min(arriveSpeed, speed);

        Vector2 desiredVelocity = arriveSpeed * distance.normalized;

        return desiredVelocity - velocity;
    }

    private Vector2 CalcSteering()
    {
        totalForce = Vector2.zero;
        arriveForce = Vector2.zero;

        // check distance from target 
        Vector2 targetPos = arriveTarget.transform.position;
        targetPos.y += offset;

        // move to player
        arriveForce = Arrive(targetPos, slowRad) * arriveWeight;

        // wander at all times 
        wanderForce = Wander(wanderRad, wanderDis, wanderJ) * wanderWeight;

        totalForce += arriveForce + evadeForce + wanderForce;
        return totalForce;
    }


    private Vector2 Evade(float timeInSeconds)
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
            desiredVelocity = desiredVelocity.normalized * speed;

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

        wanderTimer -= Time.fixedDeltaTime;

        if(wanderTimer <= 0)
        {
            wanderJitter *= Time.fixedDeltaTime;
            wanderTarget += new Vector2(
                Random.Range(-1f, 1f) * wanderJitter,
                Random.Range(-1f, 1f) * wanderJitter
                );
            wanderTarget = wanderTarget.normalized * wanderRadius;

            wanderTimer = wanderDuration;
        }

        Vector2 targetInWorldSpace = (Vector2)transform.position +
            (velocity.normalized * wanderDistance) + wanderTarget;

        return Seek(targetInWorldSpace);
    }


    protected Vector2 GetFuturePosition(float timeInSeconds)
    {
        Vector2 futurePos = (Vector2)transform.position + velocity * timeInSeconds;

        return futurePos;
    }

    public void ChangeSortOrder(int order)
    {
        sortGroup.sortingOrder = order;
    }
    public Item GetItemHeld()
    {
        if (heldObject == null) return null;

        Item held = heldObject.GetComponent<Item>(); 
        if (held != null) return held;
        
        return null;
    } 

    public void Land()
    {
        follow = false;
        
        // stop moving
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        steeringForce = Vector2.zero;

        // animation 
    }

    public void TakeOff()
    {
        follow = true;

        // animation
    }

    public void ToggleFollow()
    {
        if (PauseMovement) return;
        follow = !follow;

        // Update UI 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, arriveForce);

        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawRay(transform.position, wanderForce);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, evadeForce);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, totalForce);

        Gizmos.DrawWireSphere(GetFuturePosition(.6f), 0.25f);

    }
}
