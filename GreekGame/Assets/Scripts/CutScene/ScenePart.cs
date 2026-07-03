using UnityEngine;
using UnityEngine.Playables;

public class ScenePart : MonoBehaviour
{
    [Header("Scene Part to Play")]
    [SerializeField] private PlayableDirector thisCutscene;
    [SerializeField] private DialogueSO thisDialogue;

    [Header("After Scene Finishes")]
    [SerializeField] private GameObject nextScene;
    [SerializeField] private Player player;
    [SerializeField] private GameObject disableAfter;

    private void Awake()
    {
        // play cut scene if avaiable 
        if (thisCutscene != null)
        {
            thisCutscene.Play();
        }

        // play dialogue if avaiable 
        if (thisDialogue != null)
        {
            if (DialogueManager.Instance == null) { Debug.Log("No DialogueManager in scene"); return; }
            DialogueManager.Instance.BeginDialogue(thisDialogue, player);
        }
    }

    private void Update()
    {
        // check if scene completed 
        if (thisCutscene != null && thisCutscene.state == PlayState.Paused ||
            thisDialogue != null && !DialogueManager.Instance.DialogueIsHappening)
        {
            // play next scene 
            if (nextScene != null)
            {
                nextScene.SetActive(true);
                this.gameObject.SetActive(false);
                return;
            }

            // no next scene return to player and start tutorial
            player.gameObject.SetActive(true);
            if (TutorialManager.Instance != null) TutorialManager.Instance.Begin();
            disableAfter.SetActive(false);
            this.gameObject.SetActive(false);
        }

    }

}
