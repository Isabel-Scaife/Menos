using UnityEngine;

/// <summary>
/// a task to be part of the early-game tutorial
/// </summary>
public abstract class TutorialTask : MonoBehaviour
{
    // fields
    [TextArea(3, 6)]
    public string text;
    public Transform anchor;

    /// <summary>
    /// returns whether or not the task's conditions to be shown are met
    /// </summary>
    /// <returns>true if the task's conditions to be shown are met, false otherwise</returns>
    public abstract bool Ready();

    /// <summary>
    /// resolves logic for ending the task if its conditions are met
    /// </summary>
    /// <returns>true if task has been completed and conditions are met, false otherwise</returns>
    public abstract bool Completed();
}
