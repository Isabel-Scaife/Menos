using UnityEngine;

public class BirdTutorial : TutorialTask
{
    // fields
    private bool done;

    private void Awake()
    {
        done = false;
    }

    public override bool Ready()
    {
        return true;
    }

    public override bool Completed()
    {
        return done;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ready once bird enters trigger zone for first time
        if (collision.CompareTag("Bird"))
        {
            done = true;
        }
    }
}
