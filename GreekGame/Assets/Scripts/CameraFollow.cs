using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;

    private Transform originalTarget;

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
    }


    public void ChangeTarget(Transform target)
    {
        cinemachineCamera.Follow = target;
    }

    public void SetOrginalTarget()
    {
        ChangeTarget(originalTarget);
    }
}
