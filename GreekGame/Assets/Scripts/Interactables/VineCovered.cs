using UnityEngine;

public class VineCovered : MinigameSwapper
{
    public async override void Interact(PlayerControlled player)
    {
        await ScreenFader.Instance.FadeOut();

        VineDragMinigame minigame = followObject.GetComponent<VineDragMinigame>();
        minigame.OnComplete += HandleComplete;

        base.Interact(player);
        SetCamera();

        await ScreenFader.Instance.FadeIn();
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
