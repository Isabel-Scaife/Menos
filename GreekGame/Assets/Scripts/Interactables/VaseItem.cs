
using Unity.Cinemachine;
using UnityEngine;

public class VaseItem : Item
{
    [Header("Camera Bounds")]
    [SerializeField] private Transform vaseMinigameObject;
    [SerializeField] private PolygonCollider2D mapBounds;

    private Collider2D originalBounds;
    private CinemachineConfiner2D confiner;

    [Header("Vase instance")]
    [SerializeField] private GameObject vasePrefab;
    [SerializeField] private GameObject stampSetPrefab;

    private GameObject stampSet;
    private GameObject vase;

    private void Awake()
    {
        confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();
    }

    public async override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;


        await ScreenFader.Instance.FadeOut();

        CreateVase();

        originalBounds = confiner.BoundingShape2D;

        // set up camera
        confiner.BoundingShape2D = mapBounds;
        CameraFollow.Instance.SetTarget(vaseMinigameObject);
        CameraFollow.Instance.SetDistance(5f);

        await ScreenFader.Instance.FadeIn();

        base.Interact(player);

    }

    private void CreateVase()
    {
        vase = Instantiate(vasePrefab, vaseMinigameObject, false);
        stampSet = Instantiate(stampSetPrefab, vaseMinigameObject, false);

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
        confiner.BoundingShape2D = originalBounds;
        CameraFollow.Instance.ResetCamera();

        await ScreenFader.Instance.FadeIn();
    }
}
