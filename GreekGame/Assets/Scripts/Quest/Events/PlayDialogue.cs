using Unity.VisualScripting;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "PlayDialogue", menuName = "Events/PlayDialogue")]
public class PlayDialogue : QuestEvent
{
    [SerializeField] private DialogueSO dialogue; 

    public override void PlayEvent()
    {
        Player player = targetObject.GetComponent<Player>();

        if(dialogue != null && player != null)
        {
            if (DialogueManager.Instance == null) Debug.Log("No DialogueManager in scene");
            else DialogueManager.Instance.BeginDialogue(dialogue, player);
        }
    }
}
