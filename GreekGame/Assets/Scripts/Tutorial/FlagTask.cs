using UnityEngine;

/// <summary>
/// a tutorial task that is complete once a certain game state flag is set
/// </summary>
public class FlagTask : TutorialTask
{
    // fields
    [SerializeField]
    private string flag;

    public override bool Ready()
    {
        return true;
    }

    public override bool Completed()
    {
        // completed when given flag is set
        return GameStateManager.Instance.HasFlag(flag);
    }
}
