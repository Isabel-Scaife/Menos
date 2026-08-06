using Unity.Cinemachine;
using UnityEngine;

public class MinigameSwapper : Item
{
    private ControlsUI controlsUI;

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

    protected override void Awake()
    {
        base.Awake();
        confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();
        controlsUI = Object.FindAnyObjectByType<ControlsUI>();
    }

    protected void SetCamera()
    {
        Debug.Log("Before Confiner: " + confiner.name);
        confiner.BoundingShape2D = mapBounds;
        Debug.Log("After Confiner: " + confiner.name);
        CameraFollow.Instance.SetTarget(followObject);
        CameraFollow.Instance.SetDistance(fov);
        CameraFollow.Instance.SetOffset(offset);
        if(controlsUI != null) controlsUI.gameObject.SetActive(false);
        if (canvas != null) canvas.SetActive(true);
    }

    protected void ResetCamera()
    {
        confiner.BoundingShape2D = originalBounds;
        CameraFollow.Instance.ResetCamera();

        // resume player and bird
        playerRef.PauseMovement = false;
        if (playerRef is Player)
        {
            ((Player)playerRef).bird.PauseMovement = false;
        }

        if (canvas != null) canvas.SetActive(false);
        if (controlsUI != null) controlsUI.gameObject.SetActive(true);
    }

    public override void Interact(PlayerControlled player)
    {
        originalBounds = confiner.BoundingShape2D;

        PausePlayers(player);

        base.Interact(player);
    }

    /// <summary>
    /// Pause Player and Bird
    /// </summary>
    private void PausePlayers(PlayerControlled player)
    {
        player.PauseMovement = true;
        playerRef = player;
        if (playerRef is Player)
        {
            ((Player)playerRef).bird.PauseMovement = true;
        }
    }
}
