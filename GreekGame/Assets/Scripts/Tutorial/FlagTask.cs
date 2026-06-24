using UnityEngine;

/// <summary>
/// a tutorial task that is complete once a certain game state flag is set
/// </summary>
public class FlagTask : TutorialTask
{
    // fields
    [SerializeField]
    private string flag;

    private bool ready;

    public override bool Ready()
    {
        return ready;
    }

    public override bool Completed()
    {
        // completed when given flag is set
        return GameStateManager.Instance.HasFlag(flag);
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
