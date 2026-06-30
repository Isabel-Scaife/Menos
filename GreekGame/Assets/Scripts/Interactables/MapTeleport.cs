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

    [Header("Camera Bounds")]
    [SerializeField] private PolygonCollider2D mapBounds;

    private CinemachineConfiner2D confiner;

    [Header("Teloport Location")]
    [SerializeField] private Transform teleportTo;
    [SerializeField] private Direction direction;
    [SerializeField] private float offset = 5;

    private Vector3 newPos;

    private void Awake()
    {
        confiner = Object.FindAnyObjectByType<CinemachineConfiner2D>();

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
        await ScreenFader.Instance.FadeOut();

        // apply new camera bounds
        confiner.BoundingShape2D = mapBounds;

        // set position
        player.transform.position = newPos;

        await ScreenFader.Instance.FadeIn();
    }

    /// <summary>
    /// when player interacts with object teleport player
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(PlayerControlled player)
    {
        FadeTransition(player);
    }
}
