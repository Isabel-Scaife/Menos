using Unity.VisualScripting;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField]
    private Vector3 startingScale;
    [SerializeField]
    private Vector3 endingScale;
    private Vector3 currentScale;

    [SerializeField]
    private float growthSpeed;

    [SerializeField]
    private float maxThreshold;
    [SerializeField]
    private float growNextThreshold;

    private bool growingNext = false;
    [SerializeField]
    private bool firstLeaf = false;

    private SpriteRenderer sprite;
    private Collider2D objCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        objCollider = GetComponent<Collider2D>();
        currentScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // keep growing to reach max threshould after
        // next leaf started growing
        if(growingNext && Vector3.Distance(currentScale, endingScale) >= maxThreshold)
        {
            Grow();
        }
    }

    /// <summary>
    /// Increases size of leaf gradually, 
    /// once certain threshold hit return true
    /// </summary>
    /// <returns>
    /// true when larger than threshold for first time
    /// false otherwise
    /// </returns>
    public bool Grow()
    {
        float rate = Time.fixedDeltaTime * growthSpeed; 

        currentScale = Vector3.Lerp(currentScale, endingScale, rate);

        transform.localScale = currentScale;

        // indicates to vine to start growing next leaf
        if (!growingNext && Vector3.Distance(currentScale, endingScale) <= growNextThreshold)
        {
            growingNext = true;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Enables render and collider 
    /// </summary>
    public void TurnOn()
    {
        sprite.enabled = true;
        objCollider.enabled = true;
    }

    /// <summary>
    /// Resets leaf values 
    /// </summary>
    public void TurnOff()
    {
        growingNext = false;

        if(!firstLeaf)
        {
            // trun off visuals and collider
            sprite.enabled = false;
            objCollider.enabled = false;

            // reset size 
            currentScale = startingScale;
            transform.localScale = currentScale;

        }
    }


}
