
using UnityEngine;

public class VaseItem : MinigameSwapper
{
    [Header("Vase Instance")]
    [SerializeField] private GameObject vasePrefab;
    [SerializeField] private GameObject stampSetPrefab;

    private GameObject stampSet;
    private GameObject vase;

    public async override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

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
        Vase vaseScript = vase.GetComponent<Vase>();
        vaseScript.OnComplete += HandleComplete;
    }

    private async void HandleComplete()
    {
        await ScreenFader.Instance.FadeOut();

        // destroy vase objects
        Destroy(stampSet);
        Destroy(vase);

        // reset camera
        ResetCamera();

        await ScreenFader.Instance.FadeIn();
    }
}
