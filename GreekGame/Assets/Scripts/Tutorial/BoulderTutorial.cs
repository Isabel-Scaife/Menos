using UnityEngine;

// trigger zone that shows a message until an object has been moved far enough
public class BoulderTutorial : TutorialTask
{
    // fields
    [SerializeField]
    private Transform boulder;

    [SerializeField]
    private float distanceToMove;

    private Vector2 startPos;

    private void Awake()
    {
        startPos = boulder.position;
    }

    public override bool Completed()
    {
        // disable this once completed
        if (Vector2.Distance(startPos, (Vector2)boulder.position) >= distanceToMove)
        {
            this.gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // show popup when player collides for first time
        if (collision.CompareTag("Player") && TutorialManager.Instance != null)
        {
            TutorialManager.Instance.ShowTask(this);
        }
    }
}
