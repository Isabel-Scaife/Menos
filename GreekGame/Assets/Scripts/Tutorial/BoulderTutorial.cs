using UnityEngine;

public class BoulderTutorial : TutorialTask
{
    // fields
    [SerializeField]
    private Transform boulder;

    [SerializeField]
    private float distanceToMove;

    private Vector2 startPos;
    private bool ready;

    private void Awake()
    {
        startPos = boulder.position;
        ready = false;
    }

    public override bool Ready()
    {
        return ready;
    }

    public override bool Completed()
    {
        // returns true if boulder has moved at least target distance
        return Vector2.Distance(startPos, (Vector2)boulder.position) >= distanceToMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ready once player enters trigger zone for first time
        if (collision.CompareTag("Player"))
        {
            ready = true;
        }
    }
}
