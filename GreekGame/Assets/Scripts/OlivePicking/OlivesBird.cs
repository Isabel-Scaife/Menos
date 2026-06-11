using UnityEngine;

public class OlivesBird : MonoBehaviour
{
    // movement components
    [SerializeField]
    protected int speed = 5;
    protected Vector3 target;
    protected bool moving;

    // fields
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();

        //position da bird
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveTo(Vector3 target)
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

}
