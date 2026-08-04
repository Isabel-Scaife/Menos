using UnityEngine;

public class ChangeDialogue : MonoBehaviour, IEvent
{
    [SerializeField] private NPC npc;
    [SerializeField] private DialogueSO dialogue;
    [SerializeField] private int index = 0;

    public void OnQuestComplete()
    {
        if(npc != null && dialogue != null) 
        {
            npc.ReplaceDialogue(dialogue, 0);
        }
    }
}
