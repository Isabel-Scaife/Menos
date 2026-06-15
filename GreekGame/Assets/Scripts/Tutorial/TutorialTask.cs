using UnityEngine;

public class TutorialTask : MonoBehaviour
{
    // fields
    [TextArea(3, 6)]
    public string text;
    public Transform anchor;
    
    /// <summary>
    /// resolves logic for ending the task if its conditions are met
    /// </summary>
    /// <returns>true if task has been completed and conditions are met, false otherwise</returns>
    public virtual bool Completed()
    {
        return false;
    }

    /// <summary>
    /// show popup when player collides for first time
    /// </summary>
    /// <param name="collision">object colliding with this</param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && TutorialManager.Instance != null)
        {
            TutorialManager.Instance.ShowTask(this);
        }
    }
}
