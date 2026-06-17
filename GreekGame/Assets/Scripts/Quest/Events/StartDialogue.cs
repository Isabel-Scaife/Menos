using UnityEngine;
[System.Serializable]
public class StartDialogue : MonoBehaviour, IEvent
{
    [SerializeField] private DialogueSO dialogue;
    [SerializeField] private PlayerControlled player; // bring up to ayvin about modifiying 

    public void OnQuestComplete()
    {
        if(dialogue != null && player != null)
        {
            if (DialogueManager.Instance == null) Debug.Log("No DialogueManager in scene");
            else DialogueManager.Instance.BeginDialogue(dialogue, player);
        }
    }
}
