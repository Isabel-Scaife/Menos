using UnityEngine;

public class NPCTriggerZone : MonoBehaviour
{
    [SerializeField]
    public DialogueSO dialogue;

    [SerializeField]
    public bool disableAfter = false;

    [SerializeField]
    public Vector2 offset;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            DialogueManager.Instance.BeginDialogue(dialogue, player);

            // Teleport player
            Vector2 currentLocation = player.transform.position;
            currentLocation += offset;
            player.transform.position = currentLocation;

            // disable after 
            if (disableAfter)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
