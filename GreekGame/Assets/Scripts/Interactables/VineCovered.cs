
using UnityEngine;

public class VineCovered : MinigameSwapper
{
    [SerializeField] private DialogueSO dialogue;

    public async override void Interact(PlayerControlled player)
    {
        if (CanInteract)
        {
            await ScreenFader.Instance.FadeOut();

            VineDragMinigame minigame = followObject.GetComponent<VineDragMinigame>();
            minigame.OnComplete += HandleComplete;

            base.Interact(player);
            SetCamera();

            await ScreenFader.Instance.FadeIn();
        }
        else
        {
            if (DialogueManager.Instance == null) Debug.Log("No DialogueManager in scene");
            else DialogueManager.Instance.BeginDialogue(dialogue, player);
        }
    }

    private async void HandleComplete()
    {
        await ScreenFader.Instance.FadeOut();

        // destory minigame 
        Destroy(followObject.gameObject);

        // reset camera
        ResetCamera();

        await ScreenFader.Instance.FadeIn();
    }
}
