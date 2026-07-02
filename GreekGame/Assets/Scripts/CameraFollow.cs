using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;

    private Transform originalTarget;
    private float orginalFOV;

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
        orginalFOV = cinemachineCamera.Lens.OrthographicSize;
    }


    public void SetTarget(Transform target)
    {
        cinemachineCamera.Follow = target;
    }


    public void SetDistance(float fov)
    {
        cinemachineCamera.Lens.OrthographicSize = fov;
    }

    public void SetScreenPos(Vector2 screenPos)
    {
        // cjange sreen postion need cinemachine position composer
    }

    public void ResetCamera()
    {
        SetTarget(originalTarget);
        SetDistance(orginalFOV);
    }
}
