using UnityEngine;

/// <summary>
/// One of two guards that must be talked to to get the key to get out of jail
/// </summary>
public class NPCGuard : NPC
{
    [SerializeField]
    private GameObject key;

    /// <summary>
    /// Shows dialogue
    /// </summary>
    /// <param name="player">player interacting with this NPC</param>
    public override void Interact(PlayerControlled player)
    {
        if (dialogues != null && dialogues.Count > 0)
        {
            // start dialogue
            if (DialogueManager.Instance == null) Debug.Log("No DialogueManager in scene");
            else
            {
                DialogueManager.Instance.BeginDialogue(dialogues[0], player);
            }

            // drops key or sets up other guard to do so
            if (GameStateManager.Instance == null) Debug.Log("No GameStateManager in scene");
            else if (GameStateManager.Instance.HasFlag("talked_to_first_guard"))
            {
                key.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
                key.SetActive(true);
            }
            else
            {
                GameStateManager.Instance.SetFlag("talked_to_first_guard");
            }

        }
        Destroy(this.gameObject);
    }
}
