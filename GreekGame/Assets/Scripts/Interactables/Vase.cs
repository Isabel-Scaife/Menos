
using UnityEngine;

public class Vase : MinigameSwapper
{
    [Header("Vase Instance")]
    [SerializeField] private GameObject vasePrefab;
    [SerializeField] private GameObject stampSetPrefab;

    private GameObject stampSet;
    private GameObject vase;

    public async override void Interact(PlayerControlled player)
    {
        await ScreenFader.Instance.FadeOut();

        CreateVase();
        base.Interact(player);
        SetCamera();

        await ScreenFader.Instance.FadeIn();

    }

    private void CreateVase()
    {
        vase = Instantiate(vasePrefab, followObject, false);
        stampSet = Instantiate(stampSetPrefab, followObject, false);

        // add on complete event  
        VaseMinigame minigame = vase.GetComponent<VaseMinigame>();
        minigame.OnComplete += HandleComplete;
    }

    protected virtual async void HandleComplete()
    {
        await ScreenFader.Instance.FadeOut();

        // destroy minigame 
        Destroy(stampSet);
        Destroy(vase);

        // reset camera
        ResetCamera();

        await ScreenFader.Instance.FadeIn();
    }
}
