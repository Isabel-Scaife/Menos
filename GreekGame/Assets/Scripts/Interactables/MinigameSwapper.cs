using Unity.Cinemachine;
using UnityEngine;

public class MinigameSwapper : Item
{
    [Header("Camera Changes")]
    [SerializeField] protected Transform followObject;
    [SerializeField] private Collider2D mapBounds;
    [SerializeField] private float fov = 6;

    private Collider2D originalBounds;
    private CinemachineConfiner2D confiner;

    private PlayerControlled playerRef;

    private void Awake()
    {
        confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();
    }

    protected void SetCamera()
    {
        confiner.BoundingShape2D = mapBounds;
        CameraFollow.Instance.SetTarget(followObject);
        CameraFollow.Instance.SetDistance(fov);
    }

    protected void ResetCamera()
    {
        confiner.BoundingShape2D = originalBounds;
        CameraFollow.Instance.ResetCamera();
        playerRef.ResumeInputControls();
    }

    public override void Interact(PlayerControlled player)
    {
        originalBounds = confiner.BoundingShape2D;
        player.PauseInputControls();
        playerRef = player;
        base.Interact(player);
    }
}
