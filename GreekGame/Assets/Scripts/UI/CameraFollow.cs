using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] CinemachinePositionComposer positionComposer;

    private Transform originalTarget;
    private float originalFOV;
    private Vector2 originalOffset;

    public static CameraFollow Instance { get; private set; }
    void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        originalTarget = cinemachineCamera.Follow;
        originalFOV = cinemachineCamera.Lens.OrthographicSize;
        originalOffset = positionComposer.Composition.ScreenPosition;
    }


    public void SetTarget(Transform target)
    {
        cinemachineCamera.Follow = target;
    }


    public void SetDistance(float fov)
    {
        cinemachineCamera.Lens.OrthographicSize = fov;
    }

    public void SetOffset(Vector2 offset)
    {
        positionComposer.Composition.ScreenPosition = offset;
    }

    public void ResetCamera()
    {
        SetTarget(originalTarget);
        SetDistance(originalFOV);
        SetOffset(originalOffset);
    }
}
