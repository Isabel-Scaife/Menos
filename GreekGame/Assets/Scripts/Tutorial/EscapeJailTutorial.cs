using UnityEngine;

public class EscapeJailTutorial : TutorialTask
{
    // fields
    private bool done;
    private bool ready;

    private void Awake()
    {
        done = false;
        ready = false;
    }

    public override bool Ready()
    {
        return ready;
    }

    public override bool Completed()
    {
        return done;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // done once player enters trigger zone for first time
        if (collision.CompareTag("Player"))
        {
            done = true;
        }
    }

    // should be called once player enters separate trigger zone
    public void ShowTask()
    {
        ready = true;
    }
}
