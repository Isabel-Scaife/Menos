using UnityEngine;
using Unity.Cinemachine;


public class MapTeleport : Interactable
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        NoOffset
    }

    [SerializeField] private CinemachineConfiner2D confiner;

    [Header("Teloport Location")]
    [SerializeField] private Transform teleportTo;
    [SerializeField] private Collider2D newMapBounds;
    [SerializeField] private Direction direction;
    [SerializeField] private float offset = 5;

    private Vector3 newPos;

    private void Awake()
    {
        if(confiner == null) confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();

        // determine teleport position 
        newPos = teleportTo.position;

        switch (direction)
        {
            case Direction.Up: newPos.y += offset; break;
            case Direction.Down: newPos.y -= offset; break;
            case Direction.Left: newPos.x -= offset; break;
            case Direction.Right: newPos.x += offset; break;
        }
    }

    /// <summary>
    /// fade to black, teleport, fade back in 
    /// </summary>
    async void FadeTransition(PlayerControlled player)
    {
        player.PauseMovement = true;
        await ScreenFader.Instance.FadeOut();

        // set position
        player.transform.position = newPos;

        // apply new camera bounds
        confiner.BoundingShape2D = newMapBounds;

        await ScreenFader.Instance.FadeIn();
        player.PauseMovement = false;
    }

    /// <summary>
    /// when player interacts with object teleport player
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

        FadeTransition(player);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canInteract)
        {
            FadeTransition(collision.GetComponent<PlayerControlled>());
        }
    }
}
