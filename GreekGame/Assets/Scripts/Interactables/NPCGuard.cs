using UnityEngine;

/// <summary>
/// Guard that walks down after being interacted with
/// </summary>
public class NPCGuard : NPC
{
    [SerializeField]
    private Interactable key;
    [SerializeField]
    private NPCGuard otherGuard;
    public bool HasKey { get; set; }

    // needs other guard to give this one the key
    private void Start()
    {
        HasKey = false;
    }

    /// <summary>
    /// Shows dialogue
    /// </summary>
    /// <param name="player">player interacting with this NPC</param>
    public override void Interact(PlayerControlled player)
    {
        // exits early if no interaction is allowed at this time
        if (!canInteract) return;

        // shows dialogue, passes key to other guard if they're still there, then destroys self
        canInteract = false;
        if (dialogues != null && dialogues.Count > 0)
        {
            if (DialogueManager.Instance == null) Debug.Log("No DialogueManager in scene");
            else DialogueManager.Instance.BeginDialogue(dialogues[0], player);
        }
        if (otherGuard)
        {
            otherGuard.HasKey = true;
        }
        else
        {
            key.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            key.gameObject.SetActive(true);
        }
        Destroy(this.gameObject);
    }
}
