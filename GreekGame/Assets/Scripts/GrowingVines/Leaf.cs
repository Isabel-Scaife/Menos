using Unity.VisualScripting;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField]
    private Vector3 startingScale;
    [SerializeField]
    private Vector3 endingScale;

    [SerializeField]
    private float growthSpeed;

    [SerializeField]
    private float maxThreshold;

    [SerializeField]
    private float growNextThreshold;

    private bool growingNext = false;


    private Vector3 currentScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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



}
