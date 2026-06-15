using UnityEngine;

/// <summary>
/// a tutorial task that is complete once a certain game state flag is set
/// </summary>
public class FlagTask : TutorialTask
{
    // fields
    [SerializeField]
    private string flag;

    public override bool Completed()
    {
        // completed when given flag is set, disables this object
        if (GameStateManager.Instance != null && GameStateManager.Instance.HasFlag(flag))
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
            Debug.Log("COLLIDED WITH FLAG TASK");

        }
    }
}
