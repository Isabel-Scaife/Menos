using UnityEngine;

public class InspectItem : MinigameSwapper
{
    [Header("Inspect Item Instance")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private MagnifyingGlass magniyingGlass;

    private GameObject item;

    public async override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

        await ScreenFader.Instance.FadeOut();

        SetUp();
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
}
