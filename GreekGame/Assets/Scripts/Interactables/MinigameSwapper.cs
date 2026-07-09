using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MinigameSwapper : Item
{
    [Header("Camera Changes")]
    [SerializeField] protected Transform followObject;
    [SerializeField] private Collider2D mapBounds;
    [SerializeField] private float fov = 6;
    private Vector2 offset = Vector2.zero;

    [Header("Minigame Canvas")]
    [SerializeField] private GameObject canvas;

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
        CameraFollow.Instance.SetOffset(offset);
        if (canvas != null) canvas.SetActive(true);
    }

    protected void ResetCamera()
    {
        confiner.BoundingShape2D = originalBounds;
        CameraFollow.Instance.ResetCamera();
        playerRef.PauseMovement = false;
        if (canvas != null) canvas.SetActive(false);
    }

    public override void Interact(PlayerControlled player)
    {
        originalBounds = confiner.BoundingShape2D;
        player.PauseMovement = true;
        playerRef = player;
        base.Interact(player);
    }
}
