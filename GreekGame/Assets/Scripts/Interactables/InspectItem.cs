using UnityEngine;

public class InspectItem : MinigameSwapper
{
    [Header("Inspect Item Instance")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private MagnifyingGlass magniyingGlass;

    private GameObject item;

    public async override void Interact(PlayerControlled player)
    {
        await ScreenFader.Instance.FadeOut();

        SetUp();

        InspectMinigame minigame = followObject.GetComponent<InspectMinigame>();
        minigame.OnComplete += HandleComplete;

        base.Interact(player);
        SetCamera();

        await ScreenFader.Instance.FadeIn();
    }

    private void SetUp()
    {
        // spawn item
        item = Instantiate(itemPrefab, followObject, false);

        // update tool 
        ToolManager toolManger = FindAnyObjectByType<ToolManager>();
        magniyingGlass.SelectTool();
    }

    private async void HandleComplete()
    {
        await ScreenFader.Instance.FadeOut();

        // destory minigame 
        Destroy(item);
        magniyingGlass.canDrop = true;
        magniyingGlass.DropTool();
        magniyingGlass.canDrop = false;

        // reset camera
        ResetCamera();

        await ScreenFader.Instance.FadeIn();
    }
}
